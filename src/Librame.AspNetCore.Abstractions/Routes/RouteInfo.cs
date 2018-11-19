#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Routes
{
    /// <summary>
    /// 路由信息。
    /// </summary>
    public class RouteInfo
    {
        /// <summary>
        /// 编号。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 动作。
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 控制器。
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; set; }
    }
}
