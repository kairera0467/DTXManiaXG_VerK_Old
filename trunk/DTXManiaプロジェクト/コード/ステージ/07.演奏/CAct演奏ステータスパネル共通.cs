using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using FDK;

namespace DTXMania
{
    internal class CAct演奏ステータスパネル共通 : CActivity
    {
        // コンストラクタ
        public CAct演奏ステータスパネル共通()
        {
            base.b活性化してない = true;
        }


        // メソッド
        public void tラベル名からステータスパネルを決定する( string strラベル名 )
        {
            this.tスクリプトから難易度ラベルを取得する( strラベル名 );
        }

        // CActivity 実装

        public override void On活性化()
        {
            this.nCurrentScore = 0L;
            this.n現在のスコアGuitar = 0L;
            this.n現在のスコアBass = 0L;
            base.On活性化();
        }

        public void tスクリプトから難易度ラベルを取得する( string strラベル名 )
        {
            string strRawScriptFile;

            //ファイルの存在チェック
            if( File.Exists( CSkin.Path( @"Script\difficult.dtxs" ) ) )
            {
                //スクリプトを開く
                StreamReader reader = new StreamReader( CSkin.Path( @"Script\difficult.dtxs" ), Encoding.GetEncoding( "Shift_JIS" ) );
                strRawScriptFile = reader.ReadToEnd();

                strRawScriptFile = strRawScriptFile.Replace( Environment.NewLine, "\n" );
                string[] delimiter = { "\n" };
                string[] strSingleLine = strRawScriptFile.Split( delimiter, StringSplitOptions.RemoveEmptyEntries );

                for( int i = 0; i < strSingleLine.Length; i++ )
                {
                    if( strSingleLine[ i ].StartsWith( "//" ) )
                        continue; //コメント行の場合は無視

                    //まずSplit
                    string[] arScriptLine = strSingleLine[ i ].Split( ',' );

                    if( ( arScriptLine.Length >= 4 && arScriptLine.Length <= 5 ) == false )
                        continue; //引数が4つか5つじゃなければ無視。

                    if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.A )
                    {
                        if( arScriptLine[ 0 ] != "7" )
                            continue; //使用するシーンが違うなら無視。
                    }
                    else if( CDTXMania.ConfigIni.eNamePlate == Eタイプ.B )
                    {
                        if( arScriptLine[ 0 ] != "71" )
                            continue; //使用するシーンが違うなら無視。
                    }

                    if( arScriptLine.Length == 4 )
                    {
                        if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                            continue; //ラベル名が違うなら無視。大文字小文字区別しない
                    }
                    else if( arScriptLine.Length == 5 )
                    {
                        if( arScriptLine[ 4 ] == "1" )
                        {
                            if( arScriptLine[ 1 ] != strラベル名 )
                                continue; //ラベル名が違うなら無視。
                        }
                        else
                        {
                            if( String.Compare( arScriptLine[ 1 ], strラベル名, true ) != 0 )
                                continue; //ラベル名が違うなら無視。大文字小文字区別しない
                        }
                    }
                    this.rectDiffPanelPoint.X = Convert.ToInt32( arScriptLine[ 2 ] );
                    this.rectDiffPanelPoint.Y = Convert.ToInt32( arScriptLine[ 3 ] );

                    reader.Close();
                    break;
                }
            }
        }

        #region [ protected ]
        //-----------------
        public long nCurrentScore;
        public long n現在のスコアGuitar;
        public long n現在のスコアBass;
        protected Rectangle rectDiffPanelPoint;
        //-----------------
        #endregion
    }
}
