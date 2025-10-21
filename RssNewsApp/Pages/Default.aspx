<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RssNewsApp.Pages.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>אתר חדשות</title>

    <!-- Bootstrap עיצוב -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />

    <!--  CSS קובץ-->
    <link rel="stylesheet" href="../Styles/site.css" />

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!--  JS קובץ-->
    <script src="../Scripts/site.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <header>
            <h1>אתר חדשות</h1>
        </header>

<!-- התוכן הראשי של הדף, כולל כל רשימת החדשות -->
        <main class="container news-container">
            <!-- כפתור רענן חדשות -->
            <div class="refresh-btn text-center mb-4">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-danger" Text="רענן חדשות" OnClick="btnRefresh_Click" />
            </div>

            <!--ליצירת הכתבות בצורה דינמית repeater -->
            <asp:Repeater ID="rptNews" runat="server">
                <ItemTemplate>
                    <article class="news-card" data-id='<%# Eval("Id") %>'>
                        <div class="news-content">
                            <h4><%# Eval("Title") %></h4>
                            <time class="news-date" datetime='<%# ((DateTime)Eval("PublishDate")).ToString("yyyy-MM-ddTHH:mm") %>'>
                                <%# ((DateTime)Eval("PublishDate")).ToString("dd/MM/yyyy HH:mm") %>
                            </time>
                        </div>
                        <div class="article-body"></div>
                    </article>
                </ItemTemplate>
            </asp:Repeater>
        </main>
    </form>
</body>
</html>
