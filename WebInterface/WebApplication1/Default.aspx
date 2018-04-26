<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Timer runat="server" id="GridTimer" Interval="1000" OnTick="GridTimer_OnTick"></asp:Timer>
            <div class="row">
                <div class="col-sm-6">                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:tgaarde_dk_dbConnectionString2 %>" SelectCommand="SELECT * FROM [products]"></asp:SqlDataSource>
                    <br />    
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Height="113px" Width="436px">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                            <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                            <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
                            <asp:BoundField DataField="purchases" HeaderText="purchases" SortExpression="purchases" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:tgaarde_dk_dbConnectionString2 %>" SelectCommand="SELECT TOP 10 * FROM [scans] ORDER BY [scanned_at] DESC"></asp:SqlDataSource>
                    <br />    
                </div>
                <div class="col-sm-6" style="margin-top: 20px">                    
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Height="113px" Width="436px">
                        <Columns>
                            <asp:BoundField DataField="tag" HeaderText="tag" SortExpression="tag" />
                            <asp:BoundField DataField="scanned_at" HeaderText="scanned_at" SortExpression="scanned_at" />
                        </Columns>
                    </asp:GridView>
                    <%--<asp:Label ID="LabelTotal" runat="server" Text="Total: "></asp:Label>
                    <asp:TextBox ID="TextBoxTotal" runat="server"></asp:TextBox>
                    <asp:Button ID="ButtonGoToPayment" runat="server" Text="Continue to Payment" />--%>
                </div> 
            </div>  
        </ContentTemplate>        
    </asp:UpdatePanel>
    
    

    

</asp:Content>
