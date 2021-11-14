using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_upgrade
{
    public abstract class Product
    {
        protected string productName;
        protected Type productType;

        public void Output()
        {
            Console.WriteLine(productName);
        }
    }

    public class DesktopProduct : Product
    {
        public DesktopProduct(string productName)
        {
            this.productName = productName;
            productType = Type.Desktop;
        }
    }

    public class MobileProduct : Product
    {
        public MobileProduct(string productName)
        {
            this.productName = productName;
            productType = Type.Mobile;
        }
    }

    public class WebProduct : Product
    {
        public WebProduct(string productName)
        {
            this.productName = productName;
            productType = Type.Web;
        }
    }
}
