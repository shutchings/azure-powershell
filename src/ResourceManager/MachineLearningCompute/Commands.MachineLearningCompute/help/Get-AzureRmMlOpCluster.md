---
external help file: Microsoft.Azure.Commands.MachineLearningCompute.dll-Help.xml
Module Name: AzureRM.MachineLearningCompute
online version: 
schema: 2.0.0
---

# Get-AzureRmMlOpCluster

## SYNOPSIS
Gets an operationalization cluster object.

## SYNTAX

```
Get-AzureRmMlOpCluster [-ResourceGroupName <String>] [-Name <String>]
```

## DESCRIPTION
Gets an operationalization cluster object by name, or by resource group, or by subscription.

## EXAMPLES

### Example 1
```
PS C:\> Get-AzureRmMlOpCluster -ResourceGroupName my-group -Name my-cluster
```

Gets a specific operationalization cluster by name.

### Example 2
```
PS C:\> Get-AzureRmMlOpCluster -ResourceGroupName my-group
```

Gets all the operationalization clusters in a resource group.

### Example 3
```
PS C:\> Get-AzureRmMlOpCluster
```

Gets all the operationalization clusters in a subscription.

## PARAMETERS

### -Name
The name of the operationalization cluster.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The name of the resource group for the operationalization cluster.

```yaml
Type: String
Parameter Sets: (All)
Aliases: 

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

## INPUTS

### None


## OUTPUTS

### Microsoft.Azure.Commands.MachineLearningCompute.Models.PSOperationalizationCluster
### System.Collections.Generic.List`1[[Microsoft.Azure.Commands.MachineLearningCompute.Models.PSOperationalizationCluster, Microsoft.Azure.Commands.MachineLearningCompute, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null]]


## NOTES

## RELATED LINKS

