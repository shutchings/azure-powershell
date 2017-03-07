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

namespace Microsoft.Azure.Commands.LogicApp.Utilities
{
    /// <summary>
    /// Constant class
    /// </summary>
    public class Constants
    {
        public const string StatusEnabled = "Enabled";
        public const string StatusDisabled = "Disabled";

        public const string KeyTypeNotSpecified = "NotSpecified";
        public const string KeyTypePrimary = "Primary";
        public const string KeyTypeSecondary = "Secondary";

        public const string ApplicationServicePlanIdFormat = "/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Web/serverfarms/{2}";
    }
}