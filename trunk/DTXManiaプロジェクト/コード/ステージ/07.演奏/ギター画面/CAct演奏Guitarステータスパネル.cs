using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using FDK;

namespace DTXMania
{
	internal class CAct演奏Guitarステータスパネル : CAct演奏ステータスパネル共通
	{
        public override void On活性化()
        {
            this.ftDisplayFont = new Font("ＤＦＧ平成ゴシック体W5", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftGroupFont = new Font("ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftNameFont = new Font("Arial", 26f, FontStyle.Bold, GraphicsUnit.Pixel);
            this.ftLevelFont = new Font("Impact", 26f, FontStyle.Regular);
            this.ftDifficultyL = new Font("Arial", 30f, FontStyle.Bold);
            this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
            base.On活性化();
        }
		public override void OnManagedリソースの作成()
		{
			if( !base.b活性化してない )
			{
                this.b4font = new Bitmap(1, 1);
                Graphics graphics = Graphics.FromImage(this.b4font);
                graphics.PageUnit = GraphicsUnit.Pixel;

                this.strPlayerName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strCardName) ? "GUEST" : CDTXMania.ConfigIni.strCardName;
                this.strGroupName = string.IsNullOrEmpty(CDTXMania.ConfigIni.strGroupName) ? "" : CDTXMania.ConfigIni.strGroupName;

                this.bNamePlateL = new Bitmap(0x170, 0x103);
                graphics = Graphics.FromImage(this.bNamePlateL);

                this.iSongPanel = Image.FromFile(CSkin.Path(@"Graphics\7_songpanel.png"));
                Graphics gSongPanel = Graphics.FromImage(this.b4font);
                this.strPanelString = string.IsNullOrEmpty(CDTXMania.DTX.TITLE) ? "No Song Name" : CDTXMania.stage選曲.r確定された曲.strタイトル;
                this.bSongPanel = new Bitmap(250, 112);
                gSongPanel.Dispose();
                gSongPanel = Graphics.FromImage(this.bSongPanel);
                gSongPanel.DrawImage(this.iSongPanel, 0, 0, 250, 112);
                gSongPanel.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, (float)16f, (float)78f);

				this.tx左パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_nameplate_Guitar.png" ) );
				this.tx右パネル = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\ScreenPlay status panels right.png" ) );
                //this.tx曲名パネル = CDTXMania.tテクスチャの生成(CSkin.Path(@"Graphics\7_songpanel.png"));
                this.tx曲名パネル = new CTexture(CDTXMania.app.Device, this.bSongPanel, CDTXMania.TextureFormat, false);

                gSongPanel.Dispose();
                this.iSongPanel.Dispose();

                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if( !base.b活性化してない )
			{
				CDTXMania.tテクスチャの解放( ref this.tx左パネル );
				CDTXMania.tテクスチャの解放( ref this.tx右パネル );
                CDTXMania.tテクスチャの解放( ref this.tx曲名パネル );

                this.ftDisplayFont.Dispose();
                this.ftNameFont.Dispose();
                this.ftLevelFont.Dispose();
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if( !base.b活性化してない )
			{
				if( this.tx左パネル != null )
				{
			        this.tx左パネル.t2D描画( CDTXMania.app.Device, 337, 211);
					int guitar = CDTXMania.ConfigIni.n譜面スクロール速度.Guitar;
					if( guitar < 0 )
					{
						guitar = 0;
					}
					if( guitar > 15 )
					{
						guitar = 15;
					}
				}
				if( this.tx右パネル != null )
				{
					//this.tx右パネル.t2D描画( CDTXMania.app.Device, 0x26e, 0x143, new Rectangle( this.nStatus * 15, 0xb7, 15, 0x49 ) );
					int bass = CDTXMania.ConfigIni.n譜面スクロール速度.Bass;
					if( bass < 0 )
					{
						bass = 0;
					}
					if( bass > 15 )
					{
						bass = 15;
					}
					//this.tx右パネル.t2D描画( CDTXMania.app.Device, 0x26e, 0x35, new Rectangle( bass * 15, 0, 15, 0xac ) );
				}
                this.tx曲名パネル.t2D描画(CDTXMania.app.Device, 515, 521);
			}
			return 0;
		}


		// その他

		#region [ private ]
		//-----------------
		//[StructLayout( LayoutKind.Sequential )]
		//private struct STATUSPANEL
		//{
		//    public string label;
		//    public int status;
		//}

		//private int nStatus;
		//private STATUSPANEL[] stパネルマップ;
		private CTexture tx右パネル;
		private CTexture tx左パネル;
        private CTexture tx曲名パネル;
        private Bitmap b4font;
        private Bitmap bNamePlateL;
        //private Bitmap bNamePlateR;
        private Bitmap bSongPanel;
        //private Bitmap bDifficulty;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        private Font ftNameFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        //private Image iAlbum;
        //private Image iDrumspeed;
        //private Image iRisky;
        //private Image iNamePlate;
        private Image iSongPanel;
        //private Image iDifficulty;
        //private Image iScore;
        //private CTexture txDummy;
        //private CTexture txDifficulty;
		//-----------------
		#endregion
	}
}
