using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TopoEdit
{
    class ComponentManager
    {
        public static ComponentManager Instance = new ComponentManager();
        private List<TopoEdit.IComponent> m_componet = new List<TopoEdit.IComponent>();

        private ComponentManager()
        {

        }

        /// <summary>
        /// 加载该目录下的所有组件
        /// </summary>
        /// <param name="dirName"></param>
        public void LoadAll(string dirName)
        {
            //遍历该目录下的所有目录
            DirectoryInfo parentdi = new DirectoryInfo(Application.StartupPath + "\\" + dirName);
            foreach (DirectoryInfo di in parentdi.GetDirectories())        
            {
                Load(di.FullName, di.Name);//显示文件名称，并用完整目录的方式显示         
            }
        }

        public void Load(string dirName, string name)
        {
            //过滤SVN文件夹
            if (dirName.Contains(".svn"))
            {
                return;
            }

            try
            {
                System.Reflection.Assembly asd = System.Reflection.Assembly.LoadFile(dirName + "\\" + name + ".dll");
                Type type = asd.GetType(name + ".Component");
                if (type != null)
                {
                    object componentObj = System.Activator.CreateInstance(type);
                    if (componentObj != null)
                    {
                        TopoEdit.IComponent component = componentObj as TopoEdit.IComponent;
                        component.Load();
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("指定文件" + name + ".dll 不存在");
            }
        }

        public void UnLoad(string name)
        {
            
        }
    }
}
