Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports System.Windows.Forms

Namespace MapArbitraryDatabaseExample

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            BindCategories()
            BindProducts()
        End Sub

        Private Sub BindCategories()
            gridControl1.DataSource = New XPCollection(session1, session1.GetClassInfo(String.Empty, "Category"))
        End Sub

        Private Sub BindProducts()
            Dim dataLayer As SimpleDataLayer = CType(session1.DataLayer, SimpleDataLayer)
            Dim provider As ConnectionProviderSql = CType(dataLayer.ConnectionProvider, ConnectionProviderSql)
            Dim tables As DBTable() = provider.GetStorageTables("Product")
            gridControl2.DataSource = New XPCollection(session1, AddClass(session1.Dictionary, tables(0)))
        End Sub
    End Class
End Namespace
