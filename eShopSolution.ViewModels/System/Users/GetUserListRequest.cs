﻿using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.Users
{
    public class GetUserListRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
