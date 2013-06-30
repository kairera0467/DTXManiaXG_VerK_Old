using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏パネル文字列 : CActivity
    {
        // コンストラクタ

        public CAct演奏パネル文字列()
        {
            this.txパネル文字 = new CTexture[2];
            ST文字位置[] st文字位置Array = new ST文字位置[11];
            ST文字位置 st文字位置 = new ST文字位置();
            st文字位置.ch = '0';
            st文字位置.pt = new Point(0, 0);
            st文字位置Array[0] = st文字位置;
            ST文字位置 st文字位置2 = new ST文字位置();
            st文字位置2.ch = '1';
            st文字位置2.pt = new Point(28, 0);
            st文字位置Array[1] = st文字位置2;
            ST文字位置 st文字位置3 = new ST文字位置();
            st文字位置3.ch = '2';
            st文字位置3.pt = new Point(56, 0);
            st文字位置Array[2] = st文字位置3;
            ST文字位置 st文字位置4 = new ST文字位置();
            st文字位置4.ch = '3';
            st文字位置4.pt = new Point(84, 0);
            st文字位置Array[3] = st文字位置4;
            ST文字位置 st文字位置5 = new ST文字位置();
            st文字位置5.ch = '4';
            st文字位置5.pt = new Point(112, 0);
            st文字位置Array[4] = st文字位置5;
            ST文字位置 st文字位置6 = new ST文字位置();
            st文字位置6.ch = '5';
            st文字位置6.pt = new Point(140, 0);
            st文字位置Array[5] = st文字位置6;
            ST文字位置 st文字位置7 = new ST文字位置();
            st文字位置7.ch = '6';
            st文字位置7.pt = new Point(168, 0);
            st文字位置Array[6] = st文字位置7;
            ST文字位置 st文字位置8 = new ST文字位置();
            st文字位置8.ch = '7';
            st文字位置8.pt = new Point(196, 0);
            st文字位置Array[7] = st文字位置8;
            ST文字位置 st文字位置9 = new ST文字位置();
            st文字位置9.ch = '8';
            st文字位置9.pt = new Point(224, 0);
            st文字位置Array[8] = st文字位置9;
            ST文字位置 st文字位置10 = new ST文字位置();
            st文字位置10.ch = '9';
            st文字位置10.pt = new Point(252, 0);
            st文字位置Array[9] = st文字位置10;
            ST文字位置 st文字位置11 = new ST文字位置();
            st文字位置11.ch = '.';
            st文字位置11.pt = new Point(280, 0);
            st文字位置Array[10] = st文字位置11;
            this.st大文字位置 = st文字位置Array;

            ST文字位置[] st文字位置Array2 = new ST文字位置[12];
            ST文字位置 st文字位置13 = new ST文字位置();
            st文字位置13.ch = '0';
            st文字位置13.pt = new Point(0, 0);
            st文字位置Array2[0] = st文字位置13;
            ST文字位置 st文字位置14 = new ST文字位置();
            st文字位置14.ch = '1';
            st文字位置14.pt = new Point(20, 0);
            st文字位置Array2[1] = st文字位置14;
            ST文字位置 st文字位置15 = new ST文字位置();
            st文字位置15.ch = '2';
            st文字位置15.pt = new Point(40, 0);
            st文字位置Array2[2] = st文字位置15;
            ST文字位置 st文字位置16 = new ST文字位置();
            st文字位置16.ch = '3';
            st文字位置16.pt = new Point(60, 0);
            st文字位置Array2[3] = st文字位置16;
            ST文字位置 st文字位置17 = new ST文字位置();
            st文字位置17.ch = '4';
            st文字位置17.pt = new Point(80, 0);
            st文字位置Array2[4] = st文字位置17;
            ST文字位置 st文字位置18 = new ST文字位置();
            st文字位置18.ch = '5';
            st文字位置18.pt = new Point(100, 0);
            st文字位置Array2[5] = st文字位置18;
            ST文字位置 st文字位置19 = new ST文字位置();
            st文字位置19.ch = '6';
            st文字位置19.pt = new Point(120, 0);
            st文字位置Array2[6] = st文字位置19;
            ST文字位置 st文字位置20 = new ST文字位置();
            st文字位置20.ch = '7';
            st文字位置20.pt = new Point(140, 0);
            st文字位置Array2[7] = st文字位置20;
            ST文字位置 st文字位置21 = new ST文字位置();
            st文字位置21.ch = '8';
            st文字位置21.pt = new Point(160, 0);
            st文字位置Array2[8] = st文字位置21;
            ST文字位置 st文字位置22 = new ST文字位置();
            st文字位置22.ch = '9';
            st文字位置22.pt = new Point(180, 0);
            st文字位置Array2[9] = st文字位置22;
            ST文字位置 st文字位置23 = new ST文字位置();
            st文字位置23.ch = '%';
            st文字位置23.pt = new Point(200, 0);
            st文字位置Array2[10] = st文字位置23;
            ST文字位置 st文字位置24 = new ST文字位置();
            st文字位置24.ch = '.';
            st文字位置24.pt = new Point(210, 0);
            st文字位置Array2[11] = st文字位置24;
            this.st小文字位置 = st文字位置Array2;

            base.b活性化してない = true;
            this.strパネル文字列 = "";
        }


        // メソッド

        public void SetPanelString(string str)
        {
            this.strパネル文字列 = str;
            if (base.b活性化してる)
            {
                CDTXMania.tテクスチャの解放(ref this.txPanel);
                if ((this.strパネル文字列 != null) && (this.strパネル文字列.Length > 0))
                {
                    Bitmap image = new Bitmap(1, 1);
                    Graphics graphics = Graphics.FromImage(image);
                    graphics.PageUnit = GraphicsUnit.Pixel;
                    this.n文字列の長さdot = (int)graphics.MeasureString(this.strパネル文字列, this.ft表示用フォント).Width;
                    graphics.Dispose();
                    try
                    {
                        Bitmap bitmap2 = new Bitmap(this.n文字列の長さdot, (int)this.ft表示用フォント.Size);
                        graphics = Graphics.FromImage(bitmap2);
                        graphics.DrawString(this.strパネル文字列, this.ft表示用フォント, Brushes.White, (float)0f, (float)0f);
                        graphics.Dispose();
                        this.txPanel = new CTexture(CDTXMania.app.Device, bitmap2, CDTXMania.TextureFormat);
                        this.txPanel.vc拡大縮小倍率 = new Vector3(0.5f, 0.5f, 1f);
                        bitmap2.Dispose();
                    }
                    catch (CTextureCreateFailedException)
                    {
                        Trace.TraceError("パネル文字列テクスチャの生成に失敗しました。");
                        this.txPanel = null;
                    }
                    this.ct進行用 = new CCounter(-278, this.n文字列の長さdot / 2, 8, CDTXMania.Timer);
                }
            }
        }


        // CActivity 実装

        public override void On活性化()
        {
            this.ft表示用フォント = new Font("ＤＦＧ平成ゴシック体W7", 38f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftSongNameFont = new System.Drawing.Font("ＤＦＧ平成ゴシック体W5", 20f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.txスキルパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_SkillPanel.png"));
            this.txパネル文字[0] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Ratenumber_s.png"));
            this.txパネル文字[1] = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_Ratenumber_l.png"));
            this.n文字列の長さdot = 0;
            this.txPanel = null;
            this.ct進行用 = new CCounter();
            base.On活性化();
        }
        public override void On非活性化()
        {
            if (this.ft表示用フォント != null)
            {
                this.ft表示用フォント.Dispose();
                this.ft表示用フォント = null;
            }
            CDTXMania.tテクスチャの解放(ref this.txPanel);
            CDTXMania.tテクスチャの解放(ref this.txスキルパネル);
            CDTXMania.tテクスチャの解放(ref this.txパネル文字[0]);
            CDTXMania.tテクスチャの解放(ref this.txパネル文字[1]);
            CDTXMania.tテクスチャの解放(ref this.txジャケット画像);
            this.ct進行用 = null;
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.SetPanelString(this.strパネル文字列);
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if (!File.Exists(path))
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"));
                }
                else
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(path);
                }
                this.bmSongNameLength = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.bmSongNameLength);
                graphics.PageUnit = GraphicsUnit.Pixel;
                this.strSongName = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
                this.nSongNamePixelLength = (int)graphics.MeasureString(this.strSongName, this.ftSongNameFont).Width;
                graphics.Dispose();
                this.bmSongNameLength.Dispose();
                Bitmap image = new Bitmap(this.nSongNamePixelLength, (int)Math.Ceiling((double)this.ftSongNameFont.GetHeight()));
                graphics = Graphics.FromImage(image);

                float y = (((float)image.Height) / 2f) - (this.ftSongNameFont.Size / 2f);
                graphics.DrawString(this.strSongName, this.ftSongNameFont, Brushes.Black, (float)2f, (float)(y + 2f));
                graphics.DrawString(this.strSongName, this.ftSongNameFont, Brushes.White, (float)0f, (float)0f);

                graphics.Dispose();
                this.txSongName = new CTexture(CDTXMania.app.Device, image, CDTXMania.TextureFormat, false);
                image.Dispose();
                this.ftSongNameFont.Dispose();

                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                CDTXMania.tテクスチャの解放(ref this.txPanel);
                CDTXMania.tテクスチャの解放(ref this.txSongName);
                CDTXMania.tテクスチャの解放(ref this.txジャケット画像);
                base.OnManagedリソースの解放();
            }
        }
        public override int On進行描画()
        {
            throw new InvalidOperationException("t進行描画(x,y)のほうを使用してください。");
        }
        public int t進行描画(int x, int y)
        {
            if (!base.b活性化してない)
            {
                //this.ct進行用.t進行Loop();
                if ((string.IsNullOrEmpty(this.strパネル文字列) || (this.txPanel == null)) || (this.ct進行用 == null))
                {
                    return 0;
                }
                float num = this.txPanel.vc拡大縮小倍率.X;
                Rectangle rectangle = new Rectangle((int)(num), 0, (int)(360f / num), (int)this.ft表示用フォント.Size);
                if (rectangle.X < 0)
                {
                    x -= (int)(rectangle.X * num);
                    rectangle.Width += rectangle.X;
                    rectangle.X = 0;
                }
                if (rectangle.Right >= this.n文字列の長さdot)
                {
                    rectangle.Width -= rectangle.Right - this.n文字列の長さdot;
                }

                this.txSongName.t2D描画(CDTXMania.app.Device, 856, 630);
                //this.txジャケット画像.vc拡大縮小倍率.X = 245.0f / ((float)this.txジャケット画像.sz画像サイズ.Width);
                //this.txジャケット画像.vc拡大縮小倍率.Y = 245.0f / ((float)this.txジャケット画像.sz画像サイズ.Height);
                //this.txジャケット画像.fZ軸中心回転 = 0.3f;
                //this.txジャケット画像.t2D描画(CDTXMania.app.Device, 960, 350, new Rectangle(0, 0, this.txジャケット画像.sz画像サイズ.Width, this.txジャケット画像.sz画像サイズ.Height));
                Matrix mat = Matrix.Identity;
                mat *= Matrix.Scaling(245.0f / this.txジャケット画像.sz画像サイズ.Width, 245.0f / this.txジャケット画像.sz画像サイズ.Height, 1f);
                mat *= Matrix.Translation(440f, -335f, 0f);
                mat *= Matrix.RotationZ(0.3f);

                this.txジャケット画像.t3D描画(CDTXMania.app.Device, mat);

                if (CDTXMania.ConfigIni.nInfoType == 1)
                {
                    this.txスキルパネル.t2D描画(CDTXMania.app.Device, 23, 242);
                    this.t小文字表示(100, 314, string.Format("{0,4:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Perfect));
                    this.t小文字表示(100, 344, string.Format("{0,4:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Great));
                    this.t小文字表示(100, 374, string.Format("{0,4:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Good));
                    this.t小文字表示(100, 404, string.Format("{0,4:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Poor));
                    this.t小文字表示(100, 434, string.Format("{0,4:###0}", CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Miss));
                    this.t小文字表示(100, 464, string.Format("{0,4:###0}", CDTXMania.stage演奏ドラム画面.actCombo.n現在のコンボ数.Drums最高値));

                    int n現在のノーツ数 = 
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Perfect +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Great +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Good +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Poor +
                    CDTXMania.stage演奏ドラム画面.nヒット数・Auto含む.Drums.Miss;

                    double dbPERFECT率 = Math.Round((100.0 * CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Perfect) / n現在のノーツ数);
                    double dbGREAT率 = Math.Round((100.0 * CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Great / n現在のノーツ数));
                    double dbGOOD率 = Math.Round((100.0 * CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Good / n現在のノーツ数));
                    double dbPOOR率 = Math.Round((100.0 * CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Poor / n現在のノーツ数));
                    double dbMISS率 = Math.Round((100.0 * CDTXMania.stage演奏ドラム画面.nヒット数・Auto含まない.Drums.Miss / n現在のノーツ数));
                    double dbMAXCOMBO率 = Math.Round((100.0 * CDTXMania.stage演奏ドラム画面.actCombo.n現在のコンボ数.Drums最高値 / n現在のノーツ数));

                    if(double.IsNaN(dbPERFECT率))
                        dbPERFECT率 = 0;
                    if(double.IsNaN(dbGREAT率))
                        dbGREAT率 = 0;
                    if (double.IsNaN(dbGOOD率))
                        dbGOOD率 = 0;
                    if (double.IsNaN(dbPOOR率))
                        dbPOOR率 = 0;
                    if (double.IsNaN(dbMISS率))
                        dbMISS率 = 0;
                    if (double.IsNaN(dbMAXCOMBO率))
                        dbMAXCOMBO率 = 0;

                    this.t小文字表示(190, 314, string.Format("{0,3:##0}%", dbPERFECT率));
                    this.t小文字表示(190, 344, string.Format("{0,3:##0}%", dbGREAT率));
                    this.t小文字表示(190, 374, string.Format("{0,3:##0}%", dbGOOD率));
                    this.t小文字表示(190, 404, string.Format("{0,3:##0}%", dbPOOR率));
                    this.t小文字表示(190, 434, string.Format("{0,3:##0}%", dbMISS率));
                    this.t小文字表示(190, 464, string.Format("{0,3:##0}%", dbMAXCOMBO率));
                    this.t大文字表示(82, 518, string.Format("{0,6:##0.00}", CDTXMania.stage演奏ドラム画面.actGraph.dbグラフ値現在_渡));
                    this.t大文字表示(114, 590, string.Format("{0,6:##0.00}", (CDTXMania.stage演奏ドラム画面.actGraph.dbグラフ値現在_渡 * (CDTXMania.DTX.LEVEL.Drums / 10.0) * 0.2)));
                }
            }
            return 0;
        }


        // その他

        #region [ private ]
        //-----------------
        [StructLayout(LayoutKind.Sequential)]
        private struct ST文字位置
        {
            public char ch;
            public Point pt;
        }
        private Bitmap bmSongNameLength;
        private int nSongNamePixelLength;
        private CCounter ct進行用;
        private Font ft表示用フォント;
        private Font ftSongNameFont;
        private int n文字列の長さdot;
        private string strパネル文字列;
        private string strSongName;
        private CTexture txPanel;
        private CTexture txスキルパネル;
        private readonly ST文字位置[] st小文字位置;
        private readonly ST文字位置[] st大文字位置;
        private CTexture[] txパネル文字;
        private CTexture txSongName;
        private CTexture txジャケット画像;

        private void t小文字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st小文字位置.Length; i++)
                {
                    if (this.st小文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 20, 26);
                        if (this.txパネル文字[0] != null)
                        {
                            this.txパネル文字[0].t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += 20;
            }
        }
        private void t大文字表示(int x, int y, string str)
        {
            foreach (char ch in str)
            {
                for (int i = 0; i < this.st大文字位置.Length; i++)
                {
                    if (this.st大文字位置[i].ch == ch)
                    {
                        Rectangle rectangle = new Rectangle(this.st大文字位置[i].pt.X, this.st大文字位置[i].pt.Y, 28, 42);
                        if (ch == '.')
                        {
                            rectangle.Width -= 18;
                        }
                        if (this.txパネル文字[1] != null)
                        {
                            this.txパネル文字[1].t2D描画(CDTXMania.app.Device, x, y, rectangle);
                        }
                        break;
                    }
                }
                x += (ch == '.' ? 8 : 29);
            }
        }

        //-----------------
        #endregion
    }
}
