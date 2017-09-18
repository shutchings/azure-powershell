﻿using Microsoft.Azure.Commands.MachineLearningCompute.Models;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Management.MachineLearningCompute;
using Microsoft.Azure.Management.MachineLearningCompute.Models;
using System;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.MachineLearningCompute.Cmdlets
{
    [Cmdlet(VerbsDiagnostic.Test, CmdletSuffix + "SystemServicesUpdateAvailability")]
    [OutputType(typeof(CheckSystemServicesUpdatesAvailableResponse))]
    public class TestAzureRmOpClusterSystemServicesUpdateAvailability: MachineLearningComputeCmdletBase
    {
        protected const string CmdletParametersParameterSet =
            "Test for update availability from cmdlet input parameters.";

        protected const string ObjectParameterSet =
            "Test for update availability from an OperationalizationCluster instance definition.";

        protected const string ResourceIdParameterSet =
            "Test for update availability from an Azure resouce id.";

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

            WriteObject(MachineLearningComputeManagementClient.OperationalizationClusters.CheckSystemServicesUpdatesAvailable(ResourceGroupName, Name));
        }
    }
}
