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

using Microsoft.Azure.Management.MachineLearningCompute.Models;
using Microsoft.Azure.Management.MachineLearningCompute;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.MachineLearningCompute.Models;
using Microsoft.WindowsAzure.Commands.Utilities.Common;

namespace Microsoft.Azure.Commands.MachineLearningCompute.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, CmdletSuffix)]
    [OutputType(typeof(PSOperationalizationCluster), typeof(List<PSOperationalizationCluster>))]
    public class GetAzureRmMlOpCluster : MachineLearningComputeCmdletBase
    {
        [Parameter(Mandatory = false, 
            HelpMessage = ResourceGroupParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(Mandatory = false, 
            HelpMessage = NameParameterHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        public override void ExecuteCmdlet()
        {
            if (!string.IsNullOrWhiteSpace(this.Name))
            {
                if (string.IsNullOrWhiteSpace(this.ResourceGroupName))
                {
                    throw new PSArgumentNullException(ResourceGroupName, Resources.MissingResourceGroupName);
                }

                WriteObject(new PSOperationalizationCluster(this.MachineLearningComputeManagementClient.OperationalizationClusters.Get(this.ResourceGroupName, this.Name)));
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.ResourceGroupName))
                {
                    WriteClusterList(MachineLearningComputeManagementClient.OperationalizationClusters.ListByResourceGroup(this.ResourceGroupName));
                }
                else
                {
                    WriteClusterList(this.MachineLearningComputeManagementClient.OperationalizationClusters.ListBySubscriptionId());
                }
            }
        }

        private void WriteClusterList(IEnumerable<OperationalizationCluster> clusters)
        {
            List<PSOperationalizationCluster> output = new List<PSOperationalizationCluster>();
            clusters.ForEach(cluster => output.Add(new PSOperationalizationCluster(cluster)));

            WriteObject(output);
        }
    }
}
