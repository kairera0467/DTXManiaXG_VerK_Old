using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
            this.ftDisplayFont = new Font( "ＤＦＧ平成ゴシック体W5", 22f, FontStyle.Regular, GraphicsUnit.Pixel );
            this.ftGroupFont = new Font( "ＤＦＧ平成ゴシック体W5", 16f, FontStyle.Regular, GraphicsUnit.Pixel );
            this.ftLevelFont = new Font( "Impact", 26f, FontStyle.Regular );
            this.ftDifficultyL = new Font( "Arial", 30f, FontStyle.Bold );
            this.ftDifficultyS = new Font( "Arial", 20f, FontStyle.Bold );
            base.On活性化();
        }

		public override void OnManagedリソースの作成()
		{
            if( !base.b活性化してない )
            {
                this.txScore = CDTXMania.tテクスチャの生成( CSkin.Path( @"Graphics\7_score numbers.png" ) );
                this.iPart = Image.FromFile( CSkin.Path( @"Graphics\7_Dummy.png" ) );
                this.iDifficulty = Image.FromFile( CSkin.Path( @"Graphics\7_Difficulty.png" ) );
                //this.iDifficulty = Image.FromFile( CSkin.Path( @"Graphics\7_Difficulty_XG.png" ) );
                //this.iPart = Image.FromFile( CSkin.Path( @"Graphics\7_Part_XG.png" ) );
    

                this.iNamePlate = Image.FromFile( CSkin.Path( @"Graphics\7_nameplate_XG.png" ) );
                this.iDrumspeed = Image.FromFile(CSkin.Path(@"Graphics\7_panel_icons.jpg"));
                this.iRisky = Image.FromFile(CSkin.Path(@"Graphics\7_panel_icons2.jpg"));

                //NamePlate画像の基礎生成。
                this.b4font = new Bitmap( 1, 1 );
                Graphics gNamePlate = Graphics.FromImage( this.b4font );
                gNamePlate.PageUnit = GraphicsUnit.Pixel;
                //gNamePlate.Dispose(); //初っ端から解放してるけどどうなんやろ。
                this.bNamePlate = new Bitmap( 0x170, 0x103 );


                gNamePlate = Graphics.FromImage(this.bNamePlate);
                gNamePlate.DrawImage(this.iNamePlate, 0, 0, 0x170, 0x103);

                this.nStrlengthbydot = ( int )gNamePlate.MeasureString( this.strPanelString, this.ftDisplayFont ).Width;

                Bitmap bmpSongTitle = new Bitmap( 1, 1 );
                #region[ 曲名 ]
                if( string.IsNullOrEmpty( CDTXMania.DTX.TITLE ) || ( !CDTXMania.bコンパクトモード ) )
                    this.strPanelString = CDTXMania.stage選曲.r現在選択中の曲.strタイトル;
                else
                    this.strPanelString = CDTXMania.DTX.TITLE;


                if( File.Exists( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" ) )
                {
                    Image imgCustomSongNameTexture;
                    imgCustomSongNameTexture = Image.FromFile( CDTXMania.DTX.strフォルダ名 + "\\TitleTexture.png" );
                    //2014.08.11 kairera0467 XG1とXG2では座標が異なるため、変数を使って対処する。
                    int x = 80;//CDTXMania.ConfigIni.eNamePlate == Eタイプ.A ? 78 : 80;
                    int y = 50;//CDTXMania.ConfigIni.eNamePlate == Eタイプ.A ? 59 : 50;
                    gNamePlate.DrawImage( imgCustomSongNameTexture, x, y, 238, 30 );
                }
                else
                {
                    if( this.nStrlengthbydot > 240 )
                    {
                        gNamePlate.ScaleTransform( 240.0f / (float)this.nStrlengthbydot, 1f, MatrixOrder.Append );
                        //if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        //{
                        //    gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f / (240f / (float)this.nStrlengthbydot), 62f);
                        //    gNamePlate.ResetTransform();
                        //}
                        //else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        {
                            gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f / (240f / (float)this.nStrlengthbydot), 54f);
                            gNamePlate.ResetTransform();
                        }
                    }
                    else
                    {
                        //if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.A)
                        //{
                        //    gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f, 65f);
                        //}
                        //else if (CDTXMania.ConfigIni.eNamePlate == Eタイプ.B)
                        {
                            gNamePlate.DrawString(this.strPanelString, this.ftDisplayFont, Brushes.White, 80f, 54f);
                        }
                    }
                }
                #endregion

                //ジャケット画像読み込み
                string path = CDTXMania.DTX.strフォルダ名 + CDTXMania.DTX.PREIMAGE;
                if( !File.Exists( path ) )
                    this.iAlbum = Image.FromFile( CSkin.Path( @"Graphics\5_preimage default.png" ) );
                else
                    this.iAlbum = Image.FromFile( path );




                #region[ オプションアイコン ]
                this.nCurrentDrumspeed = CDTXMania.ConfigIni.n譜面スクロール速度.Drums;
                //TypeA
                //TypeB
                gNamePlate.DrawImage( this.iAlbum, new Rectangle( 6, 9, 0x45, 0x4b ), new Rectangle( 0, 0, this.iAlbum.Width, this.iAlbum.Height ), GraphicsUnit.Pixel );
                gNamePlate.DrawImage( this.iDrumspeed, new Rectangle( 210, 141, 42, 48 ), new Rectangle( 0, ((this.nCurrentDrumspeed > 15 ) ? 15 : this.nCurrentDrumspeed) * 0x30, 0x2a, 0x30 ), GraphicsUnit.Pixel );
                gNamePlate.DrawImage( this.iRisky, new Rectangle( 260, 141, 42, 48 ), new Rectangle( 0, ( ( CDTXMania.ConfigIni.nRisky > 10 ) ? 10 : CDTXMania.ConfigIni.nRisky ) * 48, 42, 48 ), GraphicsUnit.Pixel );
                #endregion



                //NamePlateのテクスチャを生成する。
                this.txNamePlate = new CTexture( CDTXMania.app.Device, this.bNamePlate, CDTXMania.TextureFormat, false );

                #region[ 解放 ]
                //image
                this.iAlbum.Dispose();
                this.iNamePlate.Dispose();
                this.iDifficulty.Dispose();
                this.iPart.Dispose();
                //bitmap(未実装)
                //bmpCardName.Dispose();
                //bmpSongTitle.Dispose();

                //フォント
                this.ftDisplayFont.Dispose();
                #endregion

                base.OnManagedリソースの作成();
            }
		}
		public override void OnManagedリソースの解放()
		{
            CDTXMania.tテクスチャの解放( ref this.txNamePlate );
            

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
        private Image iDrumspeed;
        private Image iRisky;
        private Image iNamePlate;
        private Image iDifficulty;
        private Image iPart;
        private int nDifficulty;
        private int nCurrentDrumspeed;
        private int nStrlengthbydot;
        private string strPanelString;
        private CTexture txNamePlate;
        private CTexture txScore;
		//-----------------
		#endregion
	}
}
