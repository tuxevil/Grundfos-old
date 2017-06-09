<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Pager" Codebehind="Pager.ascx.cs" %>
    <span class="text1"><asp:Repeater id="rptPages" Runat="server" OnItemCommand="rptPages_ItemCommand" OnItemDataBound="rptPages_ItemDataBound">
          <HeaderTemplate>
            <div>
            </HeaderTemplate>
          <ItemTemplate>
             <asp:LinkButton ID="btnPage"
                             CommandName="ChangePage"
                             CommandArgument="<%# ((PagerItem)Container.DataItem).PageNumber %>"
                             CausesValidation="false"
                             Runat="server"
                             ><%# ((PagerItem)Container.DataItem).Name %>
             </asp:LinkButton>
             <asp:ImageButton ID="imgPage"
                             CommandName="ChangePage"
                             CommandArgument="<%# ((PagerItem)Container.DataItem).PageNumber %>"
                             CausesValidation="false"
                             Runat="server"
                             Visible="false"
                             ImageUrl="">
             </asp:ImageButton>
             <asp:Label ID="litPage" Visible="false" Text="<%# ((PagerItem)Container.DataItem).Name %>" runat="server" ></asp:Label>&nbsp;
          </ItemTemplate>
          <FooterTemplate>
             </div>
          </FooterTemplate>
     </asp:Repeater></span>