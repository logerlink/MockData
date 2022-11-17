using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MockData.Enums
{
    
    public enum Lang
    {
        [Description("中文")]
        CN,
        [Description("英文")]
        EN,
        /// <summary>
        /// 中英混合，比例：8:2
        /// </summary>
        [Description("中英混合")]
        MIXIN
    }
}
