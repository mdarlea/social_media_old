//===================================================================================
// Microsoft Developer and Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================


using System;
using Swaksoft.Domain.Seedwork.Aggregates;

namespace Swaksoft.Domain.SocialMedia.SocialModule.Aggregates.CountryAgg
{
    /// <summary>
    /// The country entity
    /// </summary>
    public class Country
        :Entity
    {
        #region Properties

        /// <summary>
        /// Get or set the Country Name
        /// </summary>
        public string CountryName { get; private set; }

        /// <summary>
        /// Get or set the Country ISO Code
        /// </summary>
        public string CountryIsoCode { get; private set; }

        #endregion

        #region Constructor

        //required by EF
        private Country() { } 

        public Country(string countryName, string countryIsoCode)
        {
            if (string.IsNullOrWhiteSpace(countryName))
                throw new ArgumentNullException(nameof(countryName));

            if (string.IsNullOrWhiteSpace(countryIsoCode))
                throw new ArgumentNullException(nameof(countryIsoCode));

            CountryName = countryName;
            CountryIsoCode = countryIsoCode;
        }

        #endregion
    }
}
