﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Management.Automation;
using Microsoft.Azure.Management.MachineLearningCompute;
using Microsoft.Azure.Management.MachineLearningCompute.Models;
using Microsoft.Azure.Commands.MachineLearningCompute.Models;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Microsoft.Azure.Commands.MachineLearningCompute.Cmdlets
{
    [Cmdlet(VerbsCommon.New, CmdletSuffix, SupportsShouldProcess = true)]
    [OutputType(typeof(PSOperationalizationCluster))]
    public class NewAzureRmMlOpCluster : MachineLearningComputeCmdletBase
    {
        protected const string CreateFromObjectParameterSet =
            "Create a new operationalization cluster from an OperationalizationCluster instance definition.";

        protected const string CreateFromCmdletParametersParameterSet =
            "Create a new operationalization cluster from cmdlet input parameters.";

        [Parameter(Mandatory = true, 
            HelpMessage = ResourceGroupParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = true, 
            HelpMessage = NameParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        // Create using an op cluster object
        [Parameter(ParameterSetName = CreateFromObjectParameterSet,
            Mandatory = true, 
            HelpMessage = ClusterParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        [Alias(ClusterInputObjectAlias)]
        public PSOperationalizationCluster InputObject { get; set; }

        // Create using cmdlet parameters
        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = true, 
            HelpMessage = LocationParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = true, 
            HelpMessage = ClusterTypeParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ClusterType { get; set; }

        // Required for non-local cluster
        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = OrchestratorTypeParameterHelpMessage)]
        public string OrchestratorType { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = ServicePrincipalNameParameterHelpMessage)]
        public string ServicePrincipalName { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = ServicePrincipalSecretParameterHelpMessage)]
        public string ServicePrincipalSecret { get; set; }

        // Additional settings for non-local cluster
        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = MasterCountParameterHelpMessage)]
        public string Description { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = MasterCountParameterHelpMessage)]
        public int? MasterCount { get; set; } 

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = AgentCountParameterHelpMessage)]
        public int? AgentCount { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = AgentCountParameterHelpMessage)]
        public string AgentVmSize { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = ETagParameterHelpMessage)]
        public string GlobalServiceConfigurationETag { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = SslStatusParameterHelpMessage)]
        public string SslStatus { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = SslCertificateParameterHelpMessage)]
        public string SslCertificate { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = SslKeyParameterHelpMessage)]
        public string SslKey { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = SslCNameParameterHelpMessage)]
        public string SslCName { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = GlobalServiceConfigurationAdditionalPropertiesHelpMessage)]
        public Hashtable GlobalServiceConfigurationAdditionalProperties;

        // BYO options
        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = StorageAccountParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string StorageAccount { get; set; }

        [Parameter(ParameterSetName = CreateFromCmdletParametersParameterSet,
            Mandatory = false,
            HelpMessage = AzureContainerRegistryParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string AzureContainerRegistry { get; set; }

        public override void ExecuteCmdlet()
        {
            if (ShouldProcess(Name, @"Creating operationalization cluster..."))
            {
                // If we have an object, go ahead and create
                if (string.Equals(this.ParameterSetName, CreateFromObjectParameterSet, StringComparison.OrdinalIgnoreCase))
                {
                    var cluster = InputObject.ConvertToOperationalizationCluster();

                    WriteObject(new PSOperationalizationCluster(MachineLearningComputeManagementClient.OperationalizationClusters.CreateOrUpdate(ResourceGroupName, Name, cluster)));
                }

                // If it's parameters create the cluster object and then create
                if (string.Equals(this.ParameterSetName, CreateFromCmdletParametersParameterSet, StringComparison.OrdinalIgnoreCase))
                {
                    var newCluster = new OperationalizationCluster();

                    newCluster.Location = Location;
                    newCluster.ClusterType = ClusterType;
                    newCluster.Description = Description;

                    if (StorageAccount != null)
                    {
                        newCluster.StorageAccount = new StorageAccountProperties(StorageAccount);
                    }

                    if (AzureContainerRegistry != null)
                    {
                        newCluster.ContainerRegistry = new ContainerRegistryProperties(AzureContainerRegistry);
                    }

                    if (GlobalServiceConfigurationETag != null)
                    {
                        newCluster.GlobalServiceConfiguration = newCluster.GlobalServiceConfiguration ?? new GlobalServiceConfiguration();
                        newCluster.GlobalServiceConfiguration.Etag = GlobalServiceConfigurationETag;
                    }

                    if (GlobalServiceConfigurationAdditionalProperties != null)
                    {
                        newCluster.GlobalServiceConfiguration = newCluster.GlobalServiceConfiguration ?? new GlobalServiceConfiguration();
                        newCluster.GlobalServiceConfiguration.AdditionalProperties = GlobalServiceConfigurationAdditionalProperties.Cast<DictionaryEntry>().ToDictionary(kvp => (string)kvp.Key, kvp => kvp.Value);
                    }

                    if (SslStatus != null)
                    {
                        newCluster.GlobalServiceConfiguration.Ssl = newCluster.GlobalServiceConfiguration.Ssl ?? new SslConfiguration();
                        newCluster.GlobalServiceConfiguration.Ssl.Status = SslStatus;
                    }

                    if (SslCertificate != null)
                    {
                        newCluster.GlobalServiceConfiguration.Ssl = newCluster.GlobalServiceConfiguration.Ssl ?? new SslConfiguration();
                        newCluster.GlobalServiceConfiguration.Ssl.Cert = SslCertificate;
                    }

                    if (SslKey != null)
                    {
                        newCluster.GlobalServiceConfiguration.Ssl = newCluster.GlobalServiceConfiguration.Ssl ?? new SslConfiguration();
                        newCluster.GlobalServiceConfiguration.Ssl.Key = SslKey;
                    }

                    if (SslCName != null)
                    {
                        newCluster.GlobalServiceConfiguration.Ssl = newCluster.GlobalServiceConfiguration.Ssl ?? new SslConfiguration();
                        newCluster.GlobalServiceConfiguration.Ssl.Cname = SslCName;
                    }

                    switch (ClusterType)
                    {
                        case Management.MachineLearningCompute.Models.ClusterType.ACS:
                            newCluster.ContainerService = new AcsClusterProperties
                            {
                                OrchestratorType = OrchestratorType,
                                OrchestratorProperties = new KubernetesClusterProperties
                                {
                                    ServicePrincipal = new ServicePrincipalProperties
                                    {
                                        ClientId = ServicePrincipalName,
                                        Secret = ServicePrincipalSecret
                                    }      
                                },
                                MasterCount = MasterCount,
                                AgentCount = AgentCount,
                                AgentVmSize = AgentVmSize
                            };

                            break;

                        case Management.MachineLearningCompute.Models.ClusterType.Local:
                            break;
                        default:
                            break;
                    }

                    WriteObject(new PSOperationalizationCluster(MachineLearningComputeManagementClient.OperationalizationClusters.CreateOrUpdate(ResourceGroupName, Name, newCluster)));
                }
            }
        }
    }
}
