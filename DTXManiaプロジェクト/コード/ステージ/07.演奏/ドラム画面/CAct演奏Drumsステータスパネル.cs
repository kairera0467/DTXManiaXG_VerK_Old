using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

using FDK;

namespace DTXMania
{
    /// <summary>
    /// ネームプレートを描画するクラス。
    /// 
    /// 課題
    /// ・角度の再調整
    /// ・フォントの変更
    /// ・画像のリサイズ(なんかアレ)
    /// ・難易度フォントの画像化
    /// ・もう少しコードを綺麗にする。特にリソース作成部分。
    /// </summary>
	internal class CAct演奏Drumsステータスパネル : CAct演奏ステータスパネル共通
	{
		// コンストラクタ
        public override void On活性化()
        {
            this.ftDisplayFont = new Font("ＤＦＧ平成ゴシック体W5", 22f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftGroupFont = new Font("ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel);
            this.ftLevelFont = new Font("Impact", 26f, FontStyle.Regular);
            this.ftDifficultyL = new Font("Arial", 30f, FontStyle.Bold);
            this.ftDifficultyS = new Font("Arial", 20f, FontStyle.Bold);
            base.On活性化();
        }

		public override void OnManagedリソースの作成()
		{
            if( !base.b活性化してない )
            {
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_score numbers.png" ) );
                this.iDifficulty = Image.FromFile( CSkin.Path( @"Graphics\7_Difficulty_XG.png" ) );
                this.iPart = Image.FromFile( CSkin.Path( @"Graphics\7_Part_XG.png" ) );
    
                this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_nameplate_XG.png" ) );

                this.b4font = new Bitmap( 1, 1 );
                Graphics gNamePlate = Graphics.FromImage( this.b4font );
                gNamePlate.PageUnit = GraphicsUnit.Pixel;

                if( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード ) )
                    this.strPanelString = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strPanelString = CDTXMania.DTX.TITLE;

                this.nStrlengthbydot = ( int )gNamePlate.MeasureString( this.strPanelString, this.ftDisplayFont ).Width;
                gNamePlate.Dispose();
                
                this.bNamePlate = new Bitmap( 0x170, 0x103 );

                gNamePlate = Graphics.FromImage( this.bNamePlate );
                gNamePlate.DrawImage( this.iNamePlate, 0, 0, 0x170, 0x103 );

                gNamePlate.Dispose();

                this.txNamePlate = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );

                this.ftDisplayFont.Dispose();
                
                base.OnManagedリソースの作成();
            }
		}
		public override void OnManagedリソースの解放()
		{
            CDTXMania.tテクスチャの解放( ref this.txNamePlate );
            CDTXMania.tテクスチャの解放( ref this.txScore );

            this.ftDisplayFont.Dispose();
            this.iDifficulty.Dispose();
            this.iNamePlate.Dispose();
            this.iPart.Dispose();

            base.OnManagedリソースの解放();
		}
		public override int On進行描画()
		{
            
            SlimDX.Matrix identity = SlimDX.Matrix.Identity;

            identity *= SlimDX.Matrix.Scaling( 0.6f, 0.95f, 1.0f );
            identity *= SlimDX.Matrix.RotationY( C変換.DegreeToRadian( -30f ) );
            identity *= SlimDX.Matrix.Translation( -481.0f, 207.0f, 0 );

/*
            identity *= SlimDX.Matrix.Translation(-1135, 150, 0);
            identity *= SlimDX.Matrix.Scaling(0.338f, 0.62f, 1f);
            identity *= SlimDX.Matrix.RotationY(-0.8f);
*/
            this.txNamePlate.t3D描画( CDTXMania.app.Device, identity );

            return 0;
		}


		// その他

		#region [ private ]
		//-----------------
        private Bitmap b4font;
        private Bitmap bNamePlate;
        private Font ftDifficultyL;
        private Font ftDifficultyS;
        private Font ftDisplayFont;
        private Font ftGroupFont;
        private Font ftLevelFont;
        private Image iAlbum;
        private Image iDrumspeed;
        private Image iRisky;
        private Image iNamePlate;
        private Image iDifficulty;
        private Image iPart;
        private int nDifficulty;
        private int nCurrentDrumspeed;
        private int nStrlengthbydot;
        private string strGroupName;
        private string strPanelString;
        private string strPlayerName;
        private CTexture txNamePlate;
        private CTexture txScore;
		//-----------------
		#endregion
	}
}
