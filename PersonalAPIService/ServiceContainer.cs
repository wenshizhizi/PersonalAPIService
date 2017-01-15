namespace PersonalAPIService
{
    using Microsoft.Owin.Hosting;
    using Owin;
    using System;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Windows.Forms;


    public partial class ServiceContainer : Form
    {
        /// <summary>
        /// 释放托管资源接口
        /// </summary>
        private IDisposable dis = null;
        
        public ServiceContainer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 服务启动/关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartup_Click(object sender, EventArgs e)
        {
            var ip = textBoxIP.Text;
            var port = textBoxPort.Text;
            var nowBtnTest = ((Button)sender).Text;

            if (nowBtnTest == "启动服务")
            {
                ((Button)sender).Text = "停止服务";

                if (ip.CheckNullOrEmptyOrWhiteSpace())
                {
                    MessageBox.Show("请输入IP");
                    return;
                }

                if (port.CheckNullOrEmptyOrWhiteSpace())
                {
                    MessageBox.Show("请输入Port");
                    return;
                }

                try
                {
                    dis = WebApp.Start("http://{0}:{1}".FormatString(ip,port), Startup =>
                    {
                        HttpConfiguration config = new HttpConfiguration();

                        //config.MessageHandlers.Add(new CustomerResponseMessageHandler());

                        // 注册特性配置路由
                        config.MapHttpAttributeRoutes();
                        
                        // 注册默认路由
                        config.Routes.MapHttpRoute(
                                   name: "DefaultApi",
                                   routeTemplate: "{controller}/{id}",                                   
                                   defaults: new {
                                       controller = "LoginSystem",                                                                         
                                       id = RouteParameter.Optional }
                               );
                        
                        //启用跨域

                        /**
                         * 请注意：
                         * EnableCorsAttribute 第一个参数 origins是配置允许跨域访问的服务地址，如果允许多个服务地址跨域访问本服务，请使用“,”隔开，如：
                         * http://192.138.1.1:9527,http://192.168.1.2:9527
                         * 既表示允许上述两个地址跨域访问本服务，另，如果是ie浏览器，使用的是jquery的话，为了接入浏览器支持（主要是ie8，9）
                         * 请设置 $.support.cors = true;
                         **/
                        config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

                        // 应用配置
                        Startup.UseWebApi(config);
                    });
                }
                catch (Exception ex)
                {
                    if (dis != null)
                        dis.Dispose();
                }
            }
            else
            {
                ((Button)sender).Text = "启动服务";
                try
                {
                    if (dis != null)
                        dis.Dispose();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
