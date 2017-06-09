using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Grundfos.StockForecast;

/// <summary>
/// Creates a paging bar with previous and next arrows, including page numbers between.
/// Only the PageCount property is required.
/// Default Step: 10
/// Start Page: 1
/// </summary>
public partial class Controls_Pager :  UserControl, IPostBackEventHandler
{
    #region PagerItem Class

    /// <summary>
    /// Internal Class To Manage Easily Page Numbers on Repeater
    /// </summary>    
    protected class PagerItem
    {
        public string Name;
        public int PageNumber;

        public PagerItem(string name, int pageNumber)
        {
            Name = name;
            PageNumber = pageNumber;
        }
    }

    #endregion

    #region Pager Event Definition

    public delegate void PageChangedEventHandler(
            object sender, PageChangedEventArgs e);

    public class PageChangedEventArgs : EventArgs
    {
        public PageChangedEventArgs(int newPageNumber)
        {
            _newPageNumber = newPageNumber;
        }

        private int _newPageNumber;
        public int NewPageNumber
        {
            get { return _newPageNumber; }
            set { _newPageNumber = value; }
        }
    }

    #endregion

    public override void DataBind()
    {
        // Creates an ArrayList to DataBind in the Repeater
        // Filled with PagerItem objects.
        if (PageCount > 1)
        {
            ArrayList pages = new ArrayList();

            // Solo si no es la primer seccion
            if (CurrentPage != 1)
                pages.Add(new PagerItem("<<", 1));

            // Solo si no es la primer pagina
            if (CurrentPage != 1)
                pages.Add(new PagerItem("<", CurrentPage - 1));

            // Numeros de paginas
            for (int i = BegSection; (i <= PageCount) && (i <= EndSection); i++)
                pages.Add(new PagerItem((i).ToString(), i));

            // Solo si no es la ultima pagina
            if (PageCount != CurrentPage)
                pages.Add(new PagerItem(">", CurrentPage + 1));

            // Solo si no es la ultima seccion
            if (CurrentPage < PageCount)
                pages.Add(new PagerItem(">>", PageCount));
        
            rptPages.DataSource = pages;
            rptPages.DataBind();
        }
        base.DataBind();
    }

    #region Public Properties

    /// <summary>
    /// Total of pages to be shown
    /// </summary>
    public int PageCount
    {
        get
        {
            if (ViewState["PageCount"] != null)
                return Convert.ToInt32(ViewState["PageCount"]);
            else
                throw new SystemException("PageCount must be defined.");
        }
        set
        {
            ViewState["PageCount"] = value;
        }
    }

    /// <summary>
    /// Current page to be marked
    /// </summary>
    public int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] != null)
                return Convert.ToInt32(ViewState["CurrentPage"]);
            else
                return 1;
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }

    /// <summary>
    /// Number of pages to show per section
    /// </summary>
    public int Step
    {
        get
        {
            if (ViewState["Step"] != null)
                return Convert.ToInt32(ViewState["Step"]);
            else
                return 10;
        }
        set
        {
            ViewState["Step"] = value;
        }
    }

    #endregion

    #region Internal Use Properties

    /// <summary>
    /// Current Section
    /// </summary>
    private int CurrentSection
    {
        get
        {
            // Get Current Section
            int sect = (int)(CurrentPage / Step);

            // If zero, should be one.
            // If current section differs from CurrentPage - 1, means we are in a next click, we should add one also.
            if (sect == 0 || (int)((CurrentPage - 1) / Step) == sect)
                return sect + 1;
            else
                return sect;
        }
    }

    /// <summary>
    /// Start of Page of Current Step
    /// </summary>
    private int BegSection
    {
        get
        {
            return ((CurrentSection * Step) - Step + 1);
        }
    }

    /// <summary>
    /// End Page of Current Step
    /// </summary>
    private int EndSection
    {
        get
        {
            return CurrentSection * Step;
        }
    }

    #endregion

    #region Events

    protected void rptPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if(PageCount > 1)
        // Shows the actual page as text, not as link.
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton imgbut = (ImageButton)e.Item.FindControl("imgPage");
                switch (((PagerItem)e.Item.DataItem).Name)
                {
                    case "<<":
                        e.Item.FindControl("imgPage").Visible = true;
                        imgbut.ImageUrl = "~/images/control_final.gif";
                        e.Item.FindControl("litPage").Visible = false;
                        e.Item.FindControl("btnPage").Visible = false;
                        break;
                    case "<":
                        e.Item.FindControl("imgPage").Visible = true;
                        imgbut.ImageUrl = "~/images/control_atras.gif";
                        e.Item.FindControl("litPage").Visible = false;
                        e.Item.FindControl("btnPage").Visible = false;
                        break;
                    case ">":
                        e.Item.FindControl("imgPage").Visible = true;
                        imgbut.ImageUrl = "~/images/control_adelante.gif";
                        e.Item.FindControl("litPage").Visible = false;
                        e.Item.FindControl("btnPage").Visible = false;
                        break;
                    case ">>":
                        e.Item.FindControl("imgPage").Visible = true;
                        imgbut.ImageUrl = "~/images/control_inicio.gif";
                        e.Item.FindControl("litPage").Visible = false;
                        e.Item.FindControl("btnPage").Visible = false;
                        break;
                }
                
                if (((PagerItem)e.Item.DataItem).PageNumber == this.CurrentPage)
                {
                    e.Item.FindControl("litPage").Visible = true;
                    e.Item.FindControl("btnPage").Visible = false;
                }
            
            }
        }
    }

    protected void rptPages_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        // Launch the PageChangedEvent when a page is clicked
        if (e.CommandName == "ChangePage")
        {
            CurrentPage = Convert.ToInt32(e.CommandArgument);
            OnPageChanged(new PageChangedEventArgs(CurrentPage));
        }
    }

    #region Exposed Event for PageChanged

    public event PageChangedEventHandler PageChanged;
    
    protected virtual void OnPageChanged(PageChangedEventArgs e)
    {
        if (PageChanged != null)
        {
            DataBind();
            PageChanged(this, e);
        }
    }

    #endregion

    #endregion

	#region IPostBackEventHandler Members

	public void RaisePostBackEvent( string eventArgument )
	{
		throw new Exception( "The method or operation is not implemented." );
	}

	#endregion
}
