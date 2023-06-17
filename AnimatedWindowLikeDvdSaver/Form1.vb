
' ########################################################################################
' #                                                                                      #
' #                                   PINOU007 DVD                                       #
' #                                                                                      #
' ########################################################################################


Public Class Form1
    Private WithEvents dvdTimer As New Timer()
    Private directionX As Integer = 1
    Private directionY As Integer = 1

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H20 ' WS_EX_TRANSPARENT
            Return cp
        End Get
    End Property

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        ' Ne rien faire pour éviter le dessin du fond
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        ' Dessiner ici le contenu du formulaire
        ' Assurez-vous de dessiner les éléments visibles sur le fond transparent

        ' Dessiner l'image dans le PictureBox
        MyBase.OnPaint(e)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load





        ' Charger les dimensions depuis le fichier
        Dim filePath As String = Application.StartupPath & "\config\size.pinou007"

        If System.IO.File.Exists(filePath) Then
            Dim fileContent As String = System.IO.File.ReadAllText(filePath)
            Dim sizeValues As String() = fileContent.Split(",")
            If sizeValues.Length = 2 Then
                Dim width As Integer
                Dim height As Integer
                If Integer.TryParse(sizeValues(0), width) AndAlso Integer.TryParse(sizeValues(1), height) Then
                    Me.Width = width
                    Me.Height = height
                Else
                    MsgBox("Invalid size values in size.pinou007.")
                End If
            Else
                MsgBox("Invalid format in size.pinou007.")
            End If
        Else
            MsgBox("The size.pinou007 file does not exist.")
        End If

        ' Charger l'image depuis le fichier
        Dim imagePath As String = Application.StartupPath & "\picture\img.png"

        If System.IO.File.Exists(imagePath) Then
            Try
                Dim imageStream As New System.IO.FileStream(imagePath, System.IO.FileMode.Open)
                PictureBox1.Image = Image.FromStream(imageStream)
                imageStream.Close()
            Catch ex As Exception
                MsgBox("Failed to load the image: " & ex.Message)
            End Try
        Else
            MsgBox("The img.png file does not exist.")
        End If

        ' Configurer le mouvement du DVD
        dvdTimer.Interval = 10 ' Intervalles de temps entre chaque déplacement
        dvdTimer.Start()
    End Sub

    Private Sub dvdTimer_Tick(sender As Object, e As EventArgs) Handles dvdTimer.Tick
        ' Déplacer le formulaire
        Me.Left += directionX
        Me.Top += directionY

        ' Inverser la direction si le formulaire atteint les bords de l'écran
        If Me.Left <= 0 Or Me.Left + Me.Width >= Screen.PrimaryScreen.WorkingArea.Width Then
            directionX *= -1
        End If

        If Me.Top <= 0 Or Me.Top + Me.Height >= Screen.PrimaryScreen.WorkingArea.Height Then
            directionY *= -1
        End If
    End Sub
End Class
