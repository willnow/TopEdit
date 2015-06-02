using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using TopoEdit.Icon;
using System.IO;

namespace TopoEdit
{
    /// <summary>
    /// 数据库辅助类
    /// </summary>
    class DBHelper
    {
        private XmlDocument m_xmlDoc;//在每个模块中中需要自己创建该实例，否则多个模块之间会互相影响，比如m_xmlDoc.Schemas.Add(null, m_blockSchemaFileName);会出现累积效果
        public static DBHelper Instance = new DBHelper();

        private static readonly string m_filePath = Application.StartupPath + "\\" + Properties.Settings.Default.DataPath;
        private static readonly string m_blockFileName = m_filePath + "\\block.xml";
        private static readonly string m_bookFileName = m_filePath + "\\book.xml";
        private static readonly string m_pageFilePath = m_filePath + "\\Page\\";
        private static readonly string m_blockSchemaFileName = m_filePath + "\\block.xsd";
        private static readonly string m_pageSchemaFileName = m_pageFilePath + "\\page.xsd";
        private static readonly string m_bookSchemaFileName = m_filePath + "\\book.xsd";

        private DBHelper()
        {
            //有意留空
        }

        public void SaveBlocks(List<Block> blocks)
        {
            string fileName = m_blockFileName;

            try
            {
                //打开并验证XML文件
                m_xmlDoc = new XmlDocument();
                m_xmlDoc.Load(fileName);
                m_xmlDoc.Schemas.Add(null, m_blockSchemaFileName);
                m_xmlDoc.Validate(null);

                //从根节点中读取所有block
                XmlNode blocksNode = m_xmlDoc.DocumentElement.SelectSingleNode("Blocks");
                if (blocksNode == null)
                {
                    //XML中不存在该节点，创建出该节点
                    blocksNode = m_xmlDoc.CreateElement("Blocks");
                    m_xmlDoc.DocumentElement.AppendChild(blocksNode);
                }
                else
                {
                    //Blocks节点已存在，不需要创建
                }

                foreach (Block block in blocks)
                {
                    XmlNode blockNode = blocksNode.SelectSingleNode("Block[Name=\'" + block.Name + "\']");
                    if (blockNode == null)
                    {
                        //XML中不存在该节点，创建出该节点
                        blockNode = m_xmlDoc.CreateNode(XmlNodeType.Element, "Block", "");
                        blocksNode.AppendChild(blockNode);
                    }
                    else
                    {
                        blockNode.InnerXml = "";
                    }

                    block.Save(blockNode);
                }

                m_xmlDoc.Validate(null);
                m_xmlDoc.Save(fileName);

                MessageBox.Show("全部保存成功");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
            }
        }

        public void SaveBlock(Block block)
        {
            string fileName = m_blockFileName;

            try
            {
                //打开并验证XML文件
                m_xmlDoc = new XmlDocument();
                m_xmlDoc.Load(fileName);
                m_xmlDoc.Schemas.Add(null, m_blockSchemaFileName);
                m_xmlDoc.Validate(null);

                //从根节点中读取所有block
                XmlNode blocksNode = m_xmlDoc.DocumentElement.SelectSingleNode("Blocks");
                if (blocksNode == null)
                {
                    //XML中不存在该节点，创建出该节点
                    blocksNode = m_xmlDoc.CreateElement("Blocks");
                    m_xmlDoc.DocumentElement.AppendChild(blocksNode);
                }
                else
                {
                    //Blocks节点已存在，不需要创建
                }

                XmlNode blockNode = blocksNode.SelectSingleNode("Block[Name=\'" + block.Name + "\']");
                if (blockNode == null)
                {
                    //XML中不存在该节点，创建出该节点
                    blockNode = m_xmlDoc.CreateNode(XmlNodeType.Element, "Block", "");
                    blocksNode.AppendChild(blockNode);
                }
                else
                {
                    blockNode.InnerXml = "";
                }

                block.Save(blockNode);

                m_xmlDoc.Validate(null);
                m_xmlDoc.Save(fileName);

                MessageBox.Show("保存" + block.Name + "成功");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
            }
        }

        public void AddBlock(Block block)
        {
            string fileName = m_blockFileName;

            try
            {
                //打开并验证XML文件
                m_xmlDoc = new XmlDocument();
                m_xmlDoc.Load(fileName);
                m_xmlDoc.Schemas.Add(null, m_blockSchemaFileName);
                m_xmlDoc.Validate(null);

                //从根节点中读取所有block
                XmlNode blocksNode = m_xmlDoc.DocumentElement.SelectSingleNode("Blocks");
                if (blocksNode == null)
                {
                    //XML中不存在该节点，创建出该节点
                    blocksNode = m_xmlDoc.CreateElement("Blocks");
                    m_xmlDoc.DocumentElement.AppendChild(blocksNode);
                }
                else
                {
                    //Blocks节点已存在，不需要创建
                }

                XmlNode blockNode = blocksNode.SelectSingleNode("Block[Name=\'" + block.Name + "\']");
                if (blockNode == null)
                {
                    //XML中不存在该节点，创建出该节点
                    blockNode = m_xmlDoc.CreateNode(XmlNodeType.Element, "Block", "");
                    blocksNode.AppendChild(blockNode);
                }
                else
                {
                    MessageBox.Show("图块" + block.Name + "已经存在, 添加失败");
                    return;
                }

                block.Save(blockNode);

                m_xmlDoc.Validate(null);
                m_xmlDoc.Save(fileName);

                MessageBox.Show("保存" + block.Name + "成功");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
            }
        }

        public void DelBlock(Block block)
        {
            string fileName = m_blockFileName;

            try
            {
                //打开并验证XML文件
                m_xmlDoc = new XmlDocument();
                m_xmlDoc.Load(fileName);
                m_xmlDoc.Schemas.Add(null, m_blockSchemaFileName);
                m_xmlDoc.Validate(null);

                //从根节点中读取所有block
                XmlNode blocksNode = m_xmlDoc.DocumentElement.SelectSingleNode("Blocks");
                if (blocksNode == null)
                {
                    //XML中不存在该节点，创建出该节点
                    blocksNode = m_xmlDoc.CreateElement("Blocks");
                    m_xmlDoc.DocumentElement.AppendChild(blocksNode);
                }
                else
                {
                    //Blocks节点已存在，不需要创建
                }

                XmlNode blockNode = blocksNode.SelectSingleNode("Block[Name=\'" + block.Name + "\']");
                if (blockNode == null)
                {
                    //XML中不存在该节点，不需要删除
                }
                else
                {
                    blocksNode.RemoveChild(blockNode);
                }

                m_xmlDoc.Validate(null);
                m_xmlDoc.Save(fileName);

                MessageBox.Show("删除" + block.Name + "成功");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
            }
        }

        public void SavePage(Page page)
        {
            string fileName = m_pageFilePath + page.Name + ".xml";
                            
            try
            {
                //打开并验证XML文件
                m_xmlDoc = new XmlDocument();
                m_xmlDoc.Load(fileName);
                m_xmlDoc.Schemas.Add(null, m_pageSchemaFileName);
                m_xmlDoc.Validate(null);

                //从根节点中读取page
                XmlNode pageNode = m_xmlDoc.DocumentElement;
                pageNode.InnerXml = "";

                
                page.Save(pageNode);

                m_xmlDoc.Validate(null);
                m_xmlDoc.Save(fileName);

                MessageBox.Show("保存" + page.Name + "成功");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
            }
        }

        public void AddPage(Page page)
        {
            //判断指定文件是否存在，如果不存在则创建
            string fileName = m_pageFilePath + page.Name + ".xml";
            if (!File.Exists(fileName))
            {
                using (FileStream file = File.Create(fileName))
                {
                    try
                    {
                        file.Close();
                        //将Page保存到XML文件
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                        xmlDoc.AppendChild(xmlDecl);
                        XmlElement xmlPageNode = xmlDoc.CreateElement("Page");
                        xmlDoc.AppendChild(xmlPageNode);
                        page.Save(xmlPageNode);
                        xmlDoc.Save(fileName);
                    }
                    catch (System.Xml.XmlException ex)
                    {
                        MessageBox.Show("读取" + fileName + "时发生错误，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
                    }
                }
            }
            else
            {
                //文件已经存在，不用在创建
                SavePage(page);
            }
        }

        internal void DelPage(Page page)
        {
            //判断指定文件是否存在，如果不存在则创建
            string fileName = m_pageFilePath + page.Name + ".xml";
            File.Delete(fileName);
        }

        public void SaveBook(Book book)
        {
            string fileName = m_bookFileName;

            try
            {
                //打开并验证XML文件
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                xmlDoc.Schemas.Add(null, m_bookSchemaFileName);
                xmlDoc.Validate(null);

                //从根节点中读取所有block
                XmlNode bookNode = xmlDoc.DocumentElement;
                bookNode.InnerXml = "";

                book.Save(bookNode);

                xmlDoc.Validate(null);

                xmlDoc.Save(fileName);

                MessageBox.Show("保存" + book.Name + "成功");
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，错误原因：" + ex.Message);
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                MessageBox.Show("文件" + fileName + "格式不规范，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
                return;
            }
            catch (System.Xml.XmlException ex)
            {
                MessageBox.Show("读取" + fileName + "时发生错误，行：" + ex.LineNumber + "，列：" + ex.LinePosition + ",原因" + ex.Message);
            }
        }

        
    }
}
