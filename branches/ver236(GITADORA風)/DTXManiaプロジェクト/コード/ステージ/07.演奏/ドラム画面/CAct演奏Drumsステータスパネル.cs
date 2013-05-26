using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using SlimDX;
using FDK;

namespace DTXMania
{
    internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
    {
        public override void On活性化()
        {

            this.ftDisplayFont = new Font("ＤＦＧ平成ゴシック体W5", 22f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftGroupFont = new Font("ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftNameFont = new Font("Arial", 26f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.ftLevelFont = new Font("Impact", 26f, FontStyle.Regular);
            this.ftDifficultyL = new Font("Arial", 30f, FontStyle.Bold);
            this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
            CDTXMania.nSongDifficulty = this.nStatus;
            CDTXMania.strSongDifficulyName = this.stパネルマップ[this.nStatus].label;
            base.On活性化();
        }
        public override void OnManagedリソースの作成()
        {
            if (!base.b活性化してない)
            {
                this.txScore = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_score numbers.png"));
                this.txDifficulty = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\\7_Difficulty.png"));
                this.iDifficulty = Image.FromFile(CSkin.Path(@"Graphics\7_Dummy.png"));
                this.iDrumspeed = Image.FromFile(CSkin.Path(@"Graphics\\7_panel_icons.jpg"));
                this.iRisky = Image.FromFile(CSkin.Path(@"Graphics\\7_panel_icons2.jpg"));
                this.b4font = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.b4font);
                graphics.PageUnit = GraphicsUnit.Pixel;

                Graphics graphics2 = Graphics.FromImage(this.b4font);
                graphics2.PageUnit = GraphicsUnit.Pixel;
                this.strPanelString = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
                this.strPlayerName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strCardName) ? "GUEST" : CDTXMania.ConfigIni.strCardName;
                this.strGroupName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strGroupName) ? "" : CDTXMania.ConfigIni.strGroupName;
                this.nStrlengthbydot = (int)graphics.MeasureString(this.strPanelString, this.ftDisplayFont).Width;
                graphics.Dispose();
                this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;
                string str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Drums) / 10f); ;
                    if (CDTXMania.DTX.LEVEL.Drums > 100)
                    {
                        str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Drums) / 100f);
                    }
                    else
                    {
                        str = string.Format("{0:0.00}", ((float)CDTXMania.DTX.LEVEL.Drums) / 10f);
                    }
                
                if (CDTXMania.ConfigIni.bCLASSIC譜面判別を有効にする && (CDTXMania.DTX.bチップがある.LeftCymbal == false) && (CDTXMania.DTX.bチップがある.LP == false) && (CDTXMania.DTX.bチップがある.LBD == false) && (CDTXMania.DTX.bチップがある.FT == false) && (CDTXMania.DTX.bチップがある.Ride == false))
                {
                    str = string.Format("{0:00}", ((float)CDTXMania.DTX.LEVEL.Drums));
                }
                
                //ここで使用したフォント3つはここで開放。
                this.ftLevelFont.Dispose();
                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();

                base.OnManagedリソースの作成();
            }
        }
        public override void OnManagedリソースの解放()
        {
            if (!base.b活性化してない)
            {
                //テクスチャ5枚
                //イメージ 6枚(ジャケット画像はここで解放しない)
                //フォント 5個
                CDTXMania.tテクスチャの解放(ref this.txnameplate);
                CDTXMania.tテクスチャの解放(ref this.txNamePlate);
                CDTXMania.tテクスチャの解放(ref this.txDifficulty);
                CDTXMania.tテクスチャの解放(ref this.txDummy);
                CDTXMania.tテクスチャの解放(ref this.txScore);
                this.iDrumspeed.Dispose();
                this.iRisky.Dispose();
                this.iDifficulty.Dispose();

                this.ftDifficultyS.Dispose();
                this.ftDifficultyL.Dispose();
                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();
                this.ftLevelFont.Dispose();
                base.OnManagedリソースの解放();
            }
        }

        public override int On進行描画()
        {
            if (!base.b活性化してない)
            {

            }
            return 0;

        }


        // その他

        #region [ private ]
        //-----------------
        private Bitmap b4font;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        //private Font ftDisplayFont2;
        private Font ftNameFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private Image iDrumspeed;
        private Image iRisky;
        private Image iDifficulty;
        private Image iScore;
        private int nCurrentDrumspeed;
        private int nStrlengthbydot;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private CTexture txDummy;
        private CTexture txDifficulty;
        private CTexture txnameplate;
        private CTexture txNamePlate;
        private CTexture txScore;
        //-----------------
        #endregion
    }
}
