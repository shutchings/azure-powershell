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

using Microsoft.Azure.Commands.MachineLearningCompute.Models;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Management.MachineLearningCompute;
using Microsoft.Azure.Management.MachineLearningCompute.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.MachineLearningCompute.Cmdlets
{

    [Cmdlet(VerbsLifecycle.Invoke, CmdletSuffix + "SystemServicesUpdate", SupportsShouldProcess = true)]
    [OutputType(typeof(UpdateSystemServicesResponse))]
    public class InvokeAzureRmMlOpClusterSystemServicesUpdate : MachineLearningComputeCmdletBase
    {
        protected const string CmdletParametersParameterSet =
            "Start a system services update from cmdlet input parameters.";

        protected const string ObjectParameterSet =
            "Start a system services update from an PSOperationalizationCluster instance definition.";

        protected const string ResourceIdParameterSet =
            "Start a system services update from an Azure resouce id.";

        [Parameter(ParameterSetName = CmdletParametersParameterSet,
            Mandatory = true, 
            HelpMessage = ResourceGroupParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(ParameterSetName = CmdletParametersParameterSet,
            Mandatory = true, 
            HelpMessage = NameParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(ParameterSetName = ObjectParameterSet,
            Mandatory = true, 
            ValueFromPipeline = true,
            HelpMessage = ClusterObjectParameterHelpMessage)]
        public PSOperationalizationCluster Cluster { get; set; }

        [Parameter(ParameterSetName = ResourceIdParameterSet,
            Mandatory = true, 
            ValueFromPipelineByPropertyName = true,
            HelpMessage = ResourceIdParameterHelpMessage)]
        public string ResourceId { get; set; }

        public override void ExecuteCmdlet()
        {
            if (ShouldProcess(this.Name, @"Updating operationalization cluster's system service..."))
            {
                if (string.Equals(this.ParameterSetName, ObjectParameterSet, StringComparison.OrdinalIgnoreCase))
                {
                    var resourceInfo = new ResourceIdentifier(Cluster.Id);
                    ResourceGroupName = resourceInfo.ResourceGroupName;
                    Name = resourceInfo.ResourceName;
                }
                else if (string.Equals(this.ParameterSetName, ResourceIdParameterSet, StringComparison.OrdinalIgnoreCase))
                {
                    var resourceInfo = new ResourceIdentifier(ResourceId);
                    ResourceGroupName = resourceInfo.ResourceGroupName;
                    Name = resourceInfo.ResourceName;
                }

                WriteObject(MachineLearningComputeManagementClient.OperationalizationClusters.UpdateSystemServices(ResourceGroupName, Name));
            }
        }
    }
}
