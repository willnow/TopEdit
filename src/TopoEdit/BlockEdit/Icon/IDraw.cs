//******************************************************************************
//�ļ����� :     IDraw.cs
//��Ȩ��Ϣ :     �����ϳ�ʱ����Ϣ�������޹�˾ ��Ȩ����
//�������� :     2013-10-08
//�ļ����� :
//�޸����� :
// V1.0.0    ������    2013-10-08    �������ļ�
// V1.1.1    ������    2013-10-21    ��ɵ����ֵ����Ĺ���
// V1.2.1    ������    2013-10-22    ���±���
// V1.3.2    ������    2013-10-25    ���ݡ�CUIT������Ա��桷V1.1.1�Ĳ��Խ���޸Ĵ��룬�޸�BUG
// V1.4.4    ������    2013-11-06    �޸����Է��ֵ�BUG
// V1.5.5    ������    2013-11-18    �޸���Ԫ���ԡ�ϵͳ���Է��ֵ�BUG
// V1.6.6    ������    2013-11-22    �޸�ϵͳ���Է��ֵ�BUG
//******************************************************************************

using System.Drawing;
using System.Xml;
using CSR.ShareLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TopoEdit.EventHandler;
using TopoEdit.Visitor;
using TopoEdit.Stratege;
using System.Drawing.Drawing2D;

namespace TopoEdit.Icon
{
    public enum DockType
    {
        Left,
        Right,
        Top,
        Bottom,
    }

    /// <summary>
    /// <para>������Ƶķ�����</para>
    /// </summary>
    public abstract class IDraw : IComparable<IDraw>
    {
        /// <summary>
        /// <para>ָʾͼԪ��վ��ͼ�ϻ��Ƶ��Ⱥ�˳��m_levelֵС���Ȼ棬ֵ��ĺ�档��ʼֵΪ0��</para>
        /// </summary>
        private int m_level = 0;
        /// <summary>
        /// <para>ͼԪ�ĵ�ǰ��ʾ״̬����ʼֵΪtrue��</para>
        /// </summary>
        private bool m_visible = true;
        /// <summary>
        /// <para>ͼԪ��ͣ�����ԣ��ĸ������Ĭ�ϲ�ͣ��</para>
        /// </summary>
        private Dictionary<DockType, bool> m_dock = new Dictionary<DockType, bool>(Enum.GetValues(typeof(DockType)).Length);
        /// <summary>
        /// <para>ͣ��ʱ�Ƿ�̶���С��Ĭ�ϴ�С�̶�</para>
        /// </summary>
        private bool m_fixed = true;
        /// <summary>
        /// ͼԪ�ĸ�ͼԪ��ֱ�Ӱ�����ͼԪ��ͼԪ������ͼԪ�ĸ�ͼԪΪ��
        /// </summary>
        private IDraw m_parent = null;
        /// <summary>
        /// �ж��ཻ������
        /// </summary>
        private IntersectType m_intersectType = IntersectType.InBound;

        public IDraw Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }
        /// <summary>
        /// <para>���ͼԪ�Ļ��ƵĲ㼶���Ⱥ�˳�򣩡��㼶�ߵĺ���ƣ��㼶�͵��Ȼ��ơ�</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public virtual Int32 Level
        {
            get
            {
                return m_level;
            }
            set
            {
                m_level = value;
            }
        }

        /// <summary>
        /// <para>���ͼԪ����ʾ���ԡ�</para>
        /// </summary>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public virtual bool Visible
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = value;
            }
        }

        /// <summary>
        /// <para>ͣ������</para>
        /// </summary>
        public virtual Dictionary<DockType, bool> Dock
        {
            get 
            {
                return m_dock;
            }
            set
            {
                m_dock = value;
            }
        }

        /// <summary>
        /// <para>ͣ��ʱ�Ƿ�̶�</para>
        /// </summary>
        public virtual bool Fixed
        {
            get
            {
                return m_fixed;
            }
            set
            {
                m_fixed = value;
            }
        }

        public abstract RectangleF BoundsRect
        {
            get;
        }


        public virtual IntersectType IntersectIconType
        {
            get
            {
                return m_intersectType;
            }
            set
            {
                m_intersectType = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ͸��ɫ
        /// </summary>
        public abstract bool VisibleTransparentColor
        {
            get;
            set;
        }

        public IDraw()
        {
            m_dock.Add(DockType.Left, false);
            m_dock.Add(DockType.Right, false);
            m_dock.Add(DockType.Top, false);
            m_dock.Add(DockType.Bottom, false);
        }

        /// <summary>
        /// <para>���ݱ�ѡ�е�ͼԪ���ʹ�����ѡ�ж���</para>
        /// </summary>
        public virtual SelectedDraw CreateSelectedDraw()
        {
            return new SelectedItem(this);
        }

        /// <summary>
        /// ����ê���ƶ�ʱ���Ƿ���Խ��в�������
        /// </summary>
        /// <returns></returns>
        public bool CanZoomPart(ScaleOpMode mode)
        {
            return mode == ScaleOpMode.None;
        }

        /// <summary>
        /// ����ê���ƶ�ʱ������������ţ��ж��Ƿ����ƽ��
        /// </summary>
        /// <param name="zoom">����������ͨ���������������ж�ê������</param>
        /// <returns></returns>
        public bool CanMovePart(ScaleOpMode mode)
        {
            Debug.Assert(!CanZoomPart(mode));

            //��Ҫ�̶���С�����ƶ������ͣ������һ��ʱ������ƽ��
            if (((mode == ScaleOpMode.Up) && Dock[DockType.Top])
                    || ((mode == ScaleOpMode.Down) && Dock[DockType.Bottom])
                    || ((mode == ScaleOpMode.Left) && Dock[DockType.Left])
                    || ((mode == ScaleOpMode.Right) && Dock[DockType.Right]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// <para>�ж�������Ƿ�λ��ͼԪ�����������ڡ��÷�����ÿ��IIcon�������ʵ�֡�</para>
        /// </summary>
        /// <returns>
        /// <para></para>
        /// </returns>
        /// <remarks>
        /// <para> ������ 2013-10-08  ��������</para>
        /// </remarks>
        public abstract IDraw Intersect(Point point);
        public abstract void Draw(Graphics cGraphics, RectangleF cRect);
        public abstract bool Load(XmlNode iconNode);
        public abstract bool Save(XmlNode iconNode);
        public abstract void Zoom(Zoom zoom);
        public abstract void Rotate(Rotate rotate);
        public abstract void Move(Movement move);
        public abstract void Symmetry(Symmetry synm);
        public abstract void Accept(IDrawVisitor visitor);
        public abstract IDraw Clone();
        public abstract void Copy(IDraw src);//��Դ���ݿ�����Ŀ������
        public abstract void Round();//������Ӱ��������ʾ�ĸ�������������������
        public override string ToString()
        {
            return "";
        }

        #region IComparable<IDraw> ��Ա

        public int CompareTo(IDraw other)
        {
            if (Level < other.Level)
            {
                return -1;
            }
            else if (Level > other.Level)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}
