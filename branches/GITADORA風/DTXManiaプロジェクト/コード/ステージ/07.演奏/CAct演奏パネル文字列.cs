﻿using System;
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
                this.n曲名X = 950;
                this.n曲名Y = 630;
            }
            else if (CDTXMania.ConfigIni.bGuitar有効)
            {
                this.n曲名X = 500;
                this.n曲名Y = 630;
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
                if (string.IsNullOrEmpty(CDTXMania.DTX.TITLE) || (!CDTXMania.bコンパクトモード && CDTXMania.ConfigIni.b曲名表示をdefのものにする))
                    this.strSongName = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strSongName = CDTXMania.DTX.TITLE;

                pfタイトル = new CPrivateFastFont(new FontFamily(CDTXMania.ConfigIni.str選曲リストフォント), 20, FontStyle.Regular);
                this.ftタイトル表示用フォント = new Font(CDTXMania.ConfigIni.str選曲リストフォント, 20, FontStyle.Bold, GraphicsUnit.Pixel);

                Bitmap image1 = new Bitmap(1, 1);
                Graphics graphics1 = Graphics.FromImage(image1);

                SizeF ef = graphics1.MeasureString(this.strSongName, this.ftタイトル表示用フォント);
                Size size = new Size((int)Math.Ceiling((double)ef.Width), (int)Math.Ceiling((double)ef.Height));
                Bitmap bmpSongName = new Bitmap(size.Width, size.Height);

                bmpSongName = pfタイトル.DrawPrivateFont(this.strSongName, CPrivateFont.DrawMode.Edge, Color.Black, Color.Black, this.clGITADORAgradationTopColor, this.clGITADORAgradationBottomColor, true);
                this.txSongName = CDTXMania.tテクスチャの生成(bmpSongName, false);

                graphics1.Dispose();
                image1.Dispose();
                bmpSongName.Dispose();

                pfアーティスト = new CPrivateFastFont(new FontFamily(CDTXMania.ConfigIni.str選曲リストフォント), 15, FontStyle.Regular);
                this.ftアーティスト名表示フォント = new Font(CDTXMania.ConfigIni.str選曲リストフォント, 15, FontStyle.Bold, GraphicsUnit.Pixel);

                Bitmap image2 = new Bitmap(1, 1);
                Graphics graphics2 = Graphics.FromImage(image2);
                SizeF ef2 = graphics2.MeasureString(CDTXMania.DTX.ARTIST, this.ftアーティスト名表示フォント);
                Size size2 = new Size((int)Math.Ceiling((double)ef2.Width), (int)Math.Ceiling((double)ef2.Height));
                Bitmap bmpArtistName = new Bitmap(size2.Width, size2.Height);

                bmpArtistName = pfアーティスト.DrawPrivateFont(CDTXMania.DTX.ARTIST, CPrivateFont.DrawMode.Edge, Color.Black, Color.Black, this.clGITADORAgradationTopColor, this.clGITADORAgradationBottomColor, true);
                this.txArtistName = CDTXMania.tテクスチャの生成(bmpArtistName, false);

                graphics2.Dispose();
                image2.Dispose();
                bmpArtistName.Dispose();
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
                CDTXMania.tテクスチャの解放( ref this.txArtistName );
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

                if (this.txSongName.sz画像サイズ.Width > 320)
                    this.txSongName.vc拡大縮小倍率.X = 320f / this.txSongName.sz画像サイズ.Width;

                if (this.txArtistName.sz画像サイズ.Width > 320)
                    this.txArtistName.vc拡大縮小倍率.X = 320f / this.txArtistName.sz画像サイズ.Width;

                this.txSongName.t2D描画(CDTXMania.app.Device, this.n曲名X, this.n曲名Y);
                this.txArtistName.t2D描画(CDTXMania.app.Device, this.n曲名X, this.n曲名Y + 35);
            }
            return 0;
        }


        // その他

        #region [ private ]
        //-----------------
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
        private CTexture txArtistName;

        private Font ftタイトル表示用フォント;
        private Font ftアーティスト名表示フォント;
        private CPrivateFastFont pfタイトル;
        private CPrivateFastFont pfアーティスト;

        //2014.04.05.kairera0467 GITADORAグラデーションの色。
        //本当は共通のクラスに設置してそれを参照する形にしたかったが、なかなかいいメソッドが無いため、とりあえず個別に設置。
        private Color clGITADORAgradationTopColor = Color.FromArgb(0, 220, 200);
        private Color clGITADORAgradationBottomColor = Color.FromArgb(255, 250, 40);
        //-----------------
        #endregion
    }
}
