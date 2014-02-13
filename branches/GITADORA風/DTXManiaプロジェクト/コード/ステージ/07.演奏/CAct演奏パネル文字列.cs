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

            if (CDTXMania.ConfigIni.bDrums有効)
            {
                this.n曲名X = 856;
                this.n曲名Y = 630;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                this.n曲名X = 506;
                this.n曲名Y = 603;
            }

            this.ft表示用フォント = new Font(CDTXMania.ConfigIni.str曲名表示フォント, 26f, FontStyle.Regular, GraphicsUnit.Pixel);
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
            this.ct進行用 = null;
            base.On非活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.txジャケットパネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_JacketPanel.png"));
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if (!File.Exists(path))
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\5_preimage default.png"));
                }
                else
                {
                    this.txジャケット画像 = CDTXMania.tテクスチャの生成(path);
                }

                this.SetPanelString(this.strパネル文字列);

                #region[ 曲名、アーティスト名テクスチャの生成 ]
                this.bmSongNameLength = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.bmSongNameLength);

                graphics.PageUnit = GraphicsUnit.Pixel;

                if ( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする ) )
                    this.strSongName = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strSongName = CDTXMania.DTX.TITLE;

                graphics.Dispose();
                this.bmSongNameLength.Dispose();

                Bitmap image = new Bitmap(500, 100);
                graphics = Graphics.FromImage( image );
                
                if( CDTXMania.ConfigIni.b縮小文字のアンチエイリアスを有効にする )
                {
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;    
                }
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

                FontFamily ff = new FontFamily(CDTXMania.ConfigIni.str曲名表示フォント);
                gp.AddString(this.strSongName, ff, 1, 24, new Point(0, 0), StringFormat.GenericDefault);
                gp.AddString(CDTXMania.DTX.ARTIST, ff, 1, 20, new Point(0, 30), StringFormat.GenericDefault);


                Pen p縁 = new Pen(Color.Black, 3f);
                p縁.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                graphics.DrawPath(p縁, gp);
                graphics.FillPath(Brushes.White, gp);

                graphics.Dispose();
                ff.Dispose();

                this.txSongName = new CTexture(CDTXMania.app.Device, image, CDTXMania.TextureFormat, false);
                image.Dispose();
                #endregion
                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if ( !base.b活性化してない )
            {
                CDTXMania.tテクスチャの解放( ref this.txPanel );
                CDTXMania.tテクスチャの解放( ref this.txSongName );
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

                SlimDX.Matrix mat = SlimDX.Matrix.Identity;

                if (CDTXMania.ConfigIni.bDrums有効)
                {
                    this.nジャケットX = 985;
                    this.nジャケットY = 378;

                    //this.txジャケット画像.vc拡大縮小倍率.X = 245.0f / ((float)this.txジャケット画像.sz画像サイズ.Width);
                    //this.txジャケット画像.vc拡大縮小倍率.Y = 245.0f / ((float)this.txジャケット画像.sz画像サイズ.Height);
                    //this.txジャケット画像.fZ軸中心回転 = 0.3f;
                    //this.txジャケット画像.t2D描画(CDTXMania.app.Device, 960, 350, new Rectangle(0, 0, this.txジャケット画像.sz画像サイズ.Width, this.txジャケット画像.sz画像サイズ.Height));
                    mat *= SlimDX.Matrix.Scaling(245.0f / this.txジャケット画像.sz画像サイズ.Width, 245.0f / this.txジャケット画像.sz画像サイズ.Height, 1f);
                    mat *= SlimDX.Matrix.Translation(440f, -335f, 0f);
                    mat *= SlimDX.Matrix.RotationZ(0.3f);
                }

                if (CDTXMania.ConfigIni.bGuitar有効)
                {
                    this.nジャケットX = 467;
                    this.nジャケットY = 287;

                    mat *= SlimDX.Matrix.Scaling(245.0f / this.txジャケット画像.sz画像サイズ.Width, 245.0f / this.txジャケット画像.sz画像サイズ.Height, 1f);
                    mat *= SlimDX.Matrix.Translation(-28f, -94.5f, 0f);
                    mat *= SlimDX.Matrix.RotationZ(0.3f);
                }

                if (this.txジャケットパネル != null)
                    this.txジャケットパネル.t2D描画(CDTXMania.app.Device, this.nジャケットX, this.nジャケットY);

                if (this.txジャケット画像 != null)
                    this.txジャケット画像.t3D描画(CDTXMania.app.Device, mat);

                this.txSongName.t2D描画(CDTXMania.app.Device, this.n曲名X, this.n曲名Y);
            }
            return 0;
        }


        // その他

        #region [ private ]
        //-----------------
        private Bitmap bmSongNameLength;
        private CCounter ct進行用;
        private Font ft表示用フォント;
        private int n文字列の長さdot;
        private int n曲名X;
        private int n曲名Y;
        private int nジャケットX;
        private int nジャケットY;
        private string strパネル文字列;
        private string strSongName;
        private CTexture txPanel;
        private CTexture txジャケットパネル;
        private CTexture txジャケット画像;
        private CTexture txSongName;
        //-----------------
        #endregion
    }
}
