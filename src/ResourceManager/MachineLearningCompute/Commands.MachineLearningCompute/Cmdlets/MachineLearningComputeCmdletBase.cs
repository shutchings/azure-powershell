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

using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Models;
using Microsoft.Azure.Commands.ResourceManager.Common;
using Microsoft.Azure.Management.MachineLearningCompute;

namespace Microsoft.Azure.Commands.MachineLearningCompute.Cmdlets
{
    public abstract class MachineLearningComputeCmdletBase : AzureRMCmdlet
    {
        public const string CmdletSuffix = "AzureRmMlOpCluster";

        public const string ResourceGroupParameterHelpMessage = "The name of the resource group for the operationalization cluster.";
        public const string NameParameterHelpMessage = "The name of the operationalization cluster.";
        public const string ClusterObjectParameterHelpMessage = "The operationalization cluster object.";
        public const string ResourceIdParameterHelpMessage = "The Azure resource id for the operationalization cluster.";
        public const string ForceParameterHelpMessage = "Do not ask for confirmation.";
        public const string ClusterParameterHelpMessage = "The operationalization cluster properties.";
        public const string TagParameterHelpMessage = "The tags for the resource.";
        public const string LocationParameterHelpMessage = "The operationalization cluster's location.";
        public const string DescriptionParameterHelpMessage = "The operationalization cluster's description.";
        public const string ClusterTypeParameterHelpMessage = "The operationalization cluster type.";
        public const string OrchestratorTypeParameterHelpMessage = "The ACS cluster's orchestrator type.";
        public const string ServicePrincipalNameParameterHelpMessage = "The ACS cluster's orchestrator service principal name.";
        public const string ServicePrincipalSecretParameterHelpMessage = "The ACS cluster's orchestrator service principal secret.";
        public const string MasterCountParameterHelpMessage = "The number of master nodes in the ACS cluster.";
        public const string AgentCountParameterHelpMessage = "The number of agent nodes in the ACS cluster.";
        public const string AgentVmSizeParameterHelpMessage = "The VM size of the agent nodes in the ACS cluster.";
        public const string StorageAccountParameterHelpMessage = "The URI to the storage account to use instead of creating one.";
        public const string AzureContainerRegistryParameterHelpMessage = "The URI to the azure container registry to use instead of creating one.";
        public const string ApplicationInsightsParameterHelpMessage = "The URI to the application insights to use instead of creating one.";
        public const string ETagParameterHelpMessage = "The configuration ETag for updates.";
        public const string GlobalServiceConfigurationAdditionalPropertiesHelpMessage = "Additional properties for the global service configuration.";
        public const string SslStatusParameterHelpMessage = "SSL status. Possible values are 'Enabled' and 'Disabled'.";
        public const string SslCertificateParameterHelpMessage = "The SSL certificate data in PEM format encoded as base64 string.";
        public const string SslKeyParameterHelpMessage = "The SSL key data in PEM format encoded as base64 string.";
        public const string SslCNameParameterHelpMessage = "The CName for the SSL certificate.";

        public const string ClusterInputObjectAlias = "Cluster";

        private IMachineLearningComputeManagementClient machineLearningComputeManagementClient;

        public IMachineLearningComputeManagementClient MachineLearningComputeManagementClient
        {
            get
            {
                return machineLearningComputeManagementClient ??
                    (machineLearningComputeManagementClient =
                        AzureSession.Instance.ClientFactory.CreateArmClient<MachineLearningComputeManagementClient>(
                            DefaultProfile.DefaultContext, Common.Authentication.Abstractions.AzureEnvironment.Endpoint.ResourceManager));
            }
        }
    }
}
