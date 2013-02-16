using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Multimedia;
using DirectShowLib;

namespace FDK
{
	/// <summary>
	/// <para>DirectShow��p�����N���b�v�i����{�����j�������B</para>
	/// <para>�P�̃N���b�v�ɂ��P�� CDirectShow �C���X�^���X�𐶐�����B</para>
	/// <para>�Đ��̊J�n���~�Ȃǂ̑���̑��A�C�ӂ̎��_�ŃX�i�b�v�C���[�W���擾���邱�Ƃ��ł���B</para>
	/// </summary>
	public class CDirectShow : IDisposable
	{
		// �v���p�e�B

		public const uint WM_DSGRAPHNOTIFY = CWin32.WM_APP + 1;

		public enum E�O���t�̏�� { ���S��~��, �Đ��̂ݒ�~��, �Đ���, ���S��~�֑J�ڒ�, �Đ��̂ݒ�~�֑J�ڒ�, �Đ��֑J�ڒ�, ���� }
		public E�O���t�̏�� e�O���t�̏��
		{
			get
			{
				var status = E�O���t�̏��.����;

				if( this.MediaCtrl != null )
				{
					FilterState fs;
					int hr = this.MediaCtrl.GetState( 0, out fs );		// ����Ȃ�ɏd�����̂Œ��ӁB

					if( hr == CWin32.E_FAIL )
					{
						#region [ ���s�B]
						//-----------------
						status = E�O���t�̏��.����;
						//-----------------
						#endregion
					}
					else if( hr == CWin32.VFW_S_STATE_INTERMEDIATE )
					{
						#region [ �J�ڒ��B]
						//-----------------
						switch( fs )
						{
							case FilterState.Running:
								status = E�O���t�̏��.�Đ��֑J�ڒ�;
								break;

							case FilterState.Paused:
								status = E�O���t�̏��.�Đ��̂ݒ�~�֑J�ڒ�;
								break;

							case FilterState.Stopped:
								status = E�O���t�̏��.���S��~�֑J�ڒ�;
								break;

							default:
								status = E�O���t�̏��.����;
								break;
						}
						//-----------------
						#endregion
					}
					else
					{
						#region [ �����ԁB]
						//-----------------
						switch( fs )
						{
							case FilterState.Running:
								status = E�O���t�̏��.�Đ���;
								break;

							case FilterState.Paused:
								status = E�O���t�̏��.�Đ��̂ݒ�~��;
								break;

							case FilterState.Stopped:
								status = E�O���t�̏��.���S��~��;
								break;

							default:
								status = E�O���t�̏��.����;
								break;
						}
						//-----------------
						#endregion
					}
				}
				return status;
			}
		}
		public bool b�Đ���;
		public bool b���[�v�Đ�;

		public int n��px
		{
			get;
			protected set;
		}
		public int n����px
		{
			get;
			protected set;
		}
		public int n�X�L�������C����byte
		{
			get;
			protected set;
		}
		public int n�f�[�^�T�C�Ybyte
		{
			get;
			protected set;
		}
		public bool b�㉺���]
		{
			get;
			protected set;
		}

		public bool b�����̂�
		{
			get;
			protected set;
		}
		
		public long n���݂̃O���t�̍Đ��ʒums
		{
			get
			{
				if( this.MediaSeeking == null )
					return 0;

				long current;
				int hr = this.MediaSeeking.GetCurrentPosition( out current );
				DsError.ThrowExceptionForHR( hr );
				return (long) ( current / ( 1000.0 * 10.0 ) );
			}
		}
		/// <summary>
		/// <para>����:0�`100:�����Bset �̂݁B</para>
		/// </summary>
		public int n����
		{
			get
			{
				return this._n����;
			}
			set
			{
				if( this.BasicAudio == null )
					return;


				// �l��ۑ��B

				this._n���� = value;


				// ���j�A���ʂ��f�V�x�����ʂɕϊ��B

				int n����db = 0;

				if( value == 0 )
				{
					n����db = -10000;	// ���S����
				}
				else
				{
					n����db = (int) ( ( 20.0 * Math.Log10( ( (double) value ) / 100.0 ) ) * 100.0 );
				}


				// �f�V�x�����ʂŃO���t�̉��ʂ�ύX�B

				this.BasicAudio.put_Volume( n����db );
			}
		}
		/// <summary>
		/// <para>��:-100�`����:0�`100:�E�Bset �̂݁B</para>
		/// </summary>
		public int n�ʒu
		{
			set
			{
				if( this.BasicAudio == null )
					return;

				// ���j�A�ʒu���f�V�x���ʒu�ɕϊ��B

				int n�ʒu = Math.Min( Math.Max( value, -100 ), +100 );
				int n�ʒudb = 0;

				if( n�ʒu == 0 )
				{
					n�ʒudb = 0;
				}
				else if( n�ʒu == -100 )
				{
					n�ʒudb = -10000;
				}
				else if( n�ʒu == 100 )
				{
					n�ʒudb = +10000;
				}
				else if( n�ʒu < 0 )
				{
					n�ʒudb = (int) ( ( 20.0 * Math.Log10( ( (double) ( n�ʒu + 100 ) ) / 100.0 ) ) * 100.0 );
				}
				else
				{
					n�ʒudb = (int) ( ( -20.0 * Math.Log10( ( (double) ( 100 - n�ʒu ) ) / 100.0 ) ) * 100.0 );
				}

				// �f�V�x���ʒu�ŃO���t�̈ʒu��ύX�B

				this.BasicAudio.put_Balance( n�ʒudb );
			}
		}
		public IMediaControl MediaCtrl;
		public IMediaEventEx MediaEventEx;
		public IMediaSeeking MediaSeeking;
		public IBasicAudio BasicAudio;
		public IGraphBuilder graphBuilder;

		/// <summary>
		/// <para>CDirectShow�C���X�^���X�ɌŗL��ID�B</para>
		/// <para>DirectShow �C�x���g���E�B���h�E�ɔ��M����ہAMessageID �Ƃ��� "WM_APP+�C���X�^���XID" �𔭐M����B</para>
		/// <para>����ɂ��A�󂯑��ŃC�x���g���M�C���X�^���X����肷�邱�Ƃ��\�ɂȂ�B</para>
		/// </summary>
		public int n�C���X�^���XID
		{
			get;
			protected set;
		}


		// ���\�b�h

		public CDirectShow()
		{
		}
		public CDirectShow( string fileName, IntPtr hWnd, bool b�I�[�f�B�I�����_���Ȃ� )
		{
			// �������B

			this.n��px = 0;
			this.n����px = 0;
			this.b�㉺���] = false;
			this.n�X�L�������C����byte = 0;
			this.n�f�[�^�T�C�Ybyte = 0;
			this.b�����̂� = false;
			this.graphBuilder = null;
			this.MediaCtrl = null;
			this.b�Đ��� = false;
			this.b���[�v�Đ� = false;


			// �ÓI���X�g�ɓo�^���A�C���X�^���XID�𓾂�B

			CDirectShow.t�C���X�^���X��o�^����( this );


			// ���񏈗������B

			if( CDirectShow.n����x == 0 )	// �Z�o���܂��Ȃ�Z�o����B
				CDirectShow.n����x = Environment.ProcessorCount;	// ����x��CPU���Ƃ���B

			unsafe
			{
				this.dg���C���`��ARGB32 = new DG���C���`��[ CDirectShow.n����x ];
				this.dg���C���`��XRGB32 = new DG���C���`��[ CDirectShow.n����x ];

				for( int i = 0; i < CDirectShow.n����x; i++ )
				{
					this.dg���C���`��ARGB32[ i ] = new DG���C���`��( this.t���C���`��ARGB32 );
					this.dg���C���`��XRGB32[ i ] = new DG���C���`��( this.t���C���`��XRGB32 );
				}
			}

			try
			{
				int hr = 0;


				// �O���t�r���_�𐶐��B

				this.graphBuilder = (IGraphBuilder) new FilterGraph();
#if DEBUG
				// ROT �ւ̓o�^�B
				this.rot = new DsROTEntry( graphBuilder );
#endif


				// QueryInterface�B���݂��Ȃ���� null�B

				this.MediaCtrl = this.graphBuilder as IMediaControl;
				this.MediaEventEx = this.graphBuilder as IMediaEventEx;
				this.MediaSeeking = this.graphBuilder as IMediaSeeking;
				this.BasicAudio = this.graphBuilder as IBasicAudio;


				// IMemoryRenderer ���O���t�ɑ}���B

				AMMediaType mediaType = null;

				this.memoryRendererObject = new MemoryRenderer();
				this.memoryRenderer = (IMemoryRenderer) this.memoryRendererObject;
				var baseFilter = (IBaseFilter) this.memoryRendererObject;

				hr = this.graphBuilder.AddFilter( baseFilter, "MemoryRenderer" );
				DsError.ThrowExceptionForHR( hr );


				// fileName ����O���t�����������B

				hr = this.graphBuilder.RenderFile( fileName, null );	// IMediaControl.RenderFile() �͐�������Ȃ�
				DsError.ThrowExceptionForHR( hr );


				// �����̂݁H

				{
					IBaseFilter videoRenderer;
					IPin videoInputPin;
					CDirectShow.t�r�f�I�����_���Ƃ��̓��̓s����T���ĕԂ�( this.graphBuilder, out videoRenderer, out videoInputPin );
					if( videoRenderer == null )
						this.b�����̂� = true;
					else
					{
						C�ϊ�.tCOM�I�u�W�F�N�g���������( ref videoInputPin );
						C�ϊ�.tCOM�I�u�W�F�N�g���������( ref videoRenderer );
					}
				}


				// �C���[�W�����擾�B

				if( !this.b�����̂� )
				{
					long n;
					int m;
					this.memoryRenderer.GetWidth( out n );
					this.n��px = (int) n;
					this.memoryRenderer.GetHeight( out n );
					this.n����px = (int) n;
					this.memoryRenderer.IsBottomUp( out m );
					this.b�㉺���] = ( m != 0 );
					this.memoryRenderer.GetBufferSize( out n );
					this.n�f�[�^�T�C�Ybyte = (int) n;
					this.n�X�L�������C����byte = (int) this.n�f�[�^�T�C�Ybyte / this.n����px;
					// C����.tCOM�I�u�W�F�N�g���������( ref baseFilter );		�Ȃ񂩃L���X�g���̃I�u�W�F�N�g�܂ŉ�������̂ŉ���֎~�B
				}


				// �O���t���C������B

				if( b�I�[�f�B�I�����_���Ȃ� )
				{
					WaveFormat dummy1;
					byte[] dummy2;
					CDirectShow.t�I�[�f�B�I�����_����Null�����_���ɕς��ăt�H�[�}�b�g���擾����( this.graphBuilder, out dummy1, out dummy2 );
				}


				// ���̑��̏����B

				this.t�Đ������J�n();	// 1��ȏ� IMediaControl ���Ăяo���ĂȂ��ƁAIReferenceClock �͎擾�ł��Ȃ��B
				this.t�J�ڊ����܂ő҂��ď�Ԃ��擾����();	// ���S�� Pause �֑J�ڂ���̂�҂B�i���ˑ��j


				// �C�x���g�p�E�B���h�E�n���h����ݒ�B

				this.MediaEventEx.SetNotifyWindow( hWnd, (int) WM_DSGRAPHNOTIFY, new IntPtr( this.n�C���X�^���XID ) );
			}
#if !DEBUG
			catch( Exception e )
			{
				C�ϊ�.t��O�̏ڍׂ����O�ɏo�͂���( e );
				this.Dispose();
				throw;	// ��O���o�B
			}
#endif
			finally
			{
			}
		}

		public void t�Đ������J�n()
		{
			if( this.MediaCtrl != null )
			{
				int hr = this.MediaCtrl.Pause();		// �Đ��������J�n����B�����ł͏�������������܂ő҂��Ȃ��B
				DsError.ThrowExceptionForHR( hr );
			}
		}
		public void t�Đ��J�n()
		{
			if( this.MediaCtrl != null && --this.n�Đ��ꎞ��~�Ăяo���̗ݐω� <= 0 )
			{
				//this.t�J�ڊ����܂ő҂��ď�Ԃ��擾����();		// �Đ������i���낤�j���܂��������ĂȂ���΁A�҂B	�� �ӊO�Əd�������Ȃ̂ŊO���Ŕ��f���Ď��s����悤�ύX����B(2011.8.7)

				int hr = this.MediaCtrl.Run();					// �Đ��J�n�B
				DsError.ThrowExceptionForHR( hr );

				this.n�Đ��ꎞ��~�Ăяo���̗ݐω� = 0;		// �ꎞ��~�񐔂͂����Ń��Z�b�g�����B
				this.b�Đ��� = true;
			}
		}
		public void t�Đ��ꎞ��~()
		{
			if( this.MediaCtrl != null && this.n�Đ��ꎞ��~�Ăяo���̗ݐω� == 0 )
			{
				int hr = this.MediaCtrl.Pause();
				DsError.ThrowExceptionForHR( hr );
			}
			this.n�Đ��ꎞ��~�Ăяo���̗ݐω�++;
			this.b�Đ��� = false;
		}
		public void t�Đ���~()
		{
			if( this.MediaCtrl != null )
			{
				int hr = this.MediaCtrl.Stop();
				DsError.ThrowExceptionForHR( hr );
			}

			// ���ւ̏����B
			//this.t�Đ��ʒu��ύX����( 0.0 );		�� ���ׂ������䂷�邽�߂ɁAFDK�O���Ő��䂷��悤�ɕύX�B(2011.8.7)
			//this.t�Đ������J�n();

			this.n�Đ��ꎞ��~�Ăяo���̗ݐω� = 0;	// ��~����ƁA�ꎞ��~�Ăяo���ݐω񐔂̓��Z�b�g�����B
			this.b�Đ��� = false;
		}
		public void t�Đ��ʒu��ύX( double db�Đ��ʒums )
		{
			if( this.MediaSeeking == null )
				return;

			int hr = this.MediaSeeking.SetPositions(
				DsLong.FromInt64( (long) ( db�Đ��ʒums * 1000.0 * 10.0 ) ),
				AMSeekingSeekingFlags.AbsolutePositioning,
				null,
				AMSeekingSeekingFlags.NoPositioning );

			DsError.ThrowExceptionForHR( hr );
		}
		public void t�ŏ�����Đ��J�n()
		{
			this.t�Đ��ʒu��ύX( 0.0 );
			this.t�Đ��J�n();
		}
		public E�O���t�̏�� t�J�ڊ����܂ő҂��ď�Ԃ��擾����()
		{
			var status = E�O���t�̏��.����;

			if( this.MediaCtrl != null )
			{
				FilterState fs;
				int hr = this.MediaCtrl.GetState( 1000, out fs );	// �J�ڊ����܂ōő�1000ms�҂B
			}
			return this.e�O���t�̏��;
		}
		public unsafe void t�����_�ɂ�����ŐV�̃X�i�b�v�C���[�W��Texture�ɓ]�ʂ���( CTexture texture )
		{
			int hr;

			#region [ �Đ����ĂȂ��Ȃ牽�������A�ҁB�i�ꎞ��~����OK�B�j]
			//-----------------
			if( !this.b�Đ��� )
				return;
			//-----------------
			#endregion
			#region [ �����݂̂Ȃ牽�����Ȃ��B]
			//-----------------
			if( this.b�����̂� )
				return;
			//-----------------
			#endregion

			DataRectangle dr = texture.texture.LockRectangle( 0, LockFlags.Discard );
			try
			{
				if( this.n�X�L�������C����byte == dr.Pitch )
				{
					#region [ (A) �s�b�`�������̂ŁA�e�N�X�`���ɒ��ړ]������B]
					//-----------------
					hr = this.memoryRenderer.GetCurrentBuffer( dr.Data.DataPointer, this.n�f�[�^�T�C�Ybyte );
					DsError.ThrowExceptionForHR( hr );
					//-----------------
					#endregion
				}
				else
				{
					this.b�㉺���] = false;		// ������̕��@�ł͏�ɐ���

					#region [ (B) �s�b�`������Ȃ��̂ŁA�������ɓ]�����Ă���e�N�X�`���ɓ]������B]
					//-----------------

					#region [ IMemoryRenderer ����o�b�t�@�ɃC���[�W�f�[�^��ǂݍ��ށB]
					//-----------------
					if( this.ip == IntPtr.Zero )
						this.ip = Marshal.AllocCoTaskMem( this.n�f�[�^�T�C�Ybyte );

					hr = this.memoryRenderer.GetCurrentBuffer( this.ip, this.n�f�[�^�T�C�Ybyte );
					DsError.ThrowExceptionForHR( hr );
					//-----------------
					#endregion

					#region [ �e�N�X�`���ɃX�i�b�v�C���[�W��]���B]
					//-----------------
					bool bARGB32 = true;

					switch( texture.Format )
					{
						case Format.A8R8G8B8:
							bARGB32 = true;
							break;

						case Format.X8R8G8B8:
							bARGB32 = false;
							break;

						default:
							return;		// ���Ή��̃t�H�[�}�b�g�͖����B
					}

					// �X���b�h�v�[�����g���ĕ���]�����鏀���B

					this.ptrSnap = (byte*) this.ip.ToPointer();
					var ptr = stackalloc UInt32*[ CDirectShow.n����x ];	// stackalloc�iGC�ΏۊO�A���\�b�h�I�����Ɏ����J���j�́A�X�^�b�N�ϐ�����ɂ����g���Ȃ��B
					ptr[ 0 ] = (UInt32*) dr.Data.DataPointer.ToPointer();	//		��
					for( int i = 1; i < CDirectShow.n����x; i++ )			// �X�^�b�N�ϐ��Ŋm�ہA���������āc
						ptr[ i ] = ptr[ i - 1 ] + this.n��px;				//		��
					this.ptrTexture = ptr;									// �X�^�b�N�ϐ����N���X�����o�ɓn���i����Ȃ�OK�j�B


					// ����x���P�Ȃ�V���O���X���b�h�A�Q�ȏ�Ȃ�}���`�X���b�h�œ]������B
					// �� CPU���P�̏ꍇ�A�킴�킴�X���b�h�v�[���̃X���b�h�ŏ�������͖̂��ʁB

					if( CDirectShow.n����x == 1 )
					{
						if( bARGB32 )
							this.t���C���`��ARGB32( 0 );
						else
							this.t���C���`��XRGB32( 0 );
					}
					else
					{
						// �]���J�n�B

						var ar = new IAsyncResult[ CDirectShow.n����x ];
						for( int i = 0; i < CDirectShow.n����x; i++ )
						{
							ar[ i ] = ( bARGB32 ) ?
								this.dg���C���`��ARGB32[ i ].BeginInvoke( i, null, null ) :
								this.dg���C���`��XRGB32[ i ].BeginInvoke( i, null, null );
						}


						// �]�������҂��B

						for( int i = 0; i < CDirectShow.n����x; i++ )
						{
							if( bARGB32 )
								this.dg���C���`��ARGB32[ i ].EndInvoke( ar[ i ] );
							else
								this.dg���C���`��XRGB32[ i ].EndInvoke( ar[ i ] );
						}
					}

					this.ptrSnap = null;
					this.ptrTexture = null;
					//-----------------
					#endregion

					//-----------------
					#endregion
				}
			}
			finally
			{
				texture.texture.UnlockRectangle( 0 );
			}
		}

		private IntPtr ip = IntPtr.Zero;

		public static void t�O���t����͂��f�o�b�O�o�͂���( IGraphBuilder graphBuilder )
		{
			if( graphBuilder == null )
			{
				Debug.WriteLine( "�w�肳�ꂽ�O���t�� null �ł��B" );
				return;
			}

			int hr = 0;

			IEnumFilters eFilters;
			hr = graphBuilder.EnumFilters( out eFilters );
			DsError.ThrowExceptionForHR( hr );
			{
				var filters = new IBaseFilter[ 1 ];
				while( eFilters.Next( 1, filters, IntPtr.Zero ) == CWin32.S_OK )
				{
					FilterInfo filterInfo;
					hr = filters[ 0 ].QueryFilterInfo( out filterInfo );
					DsError.ThrowExceptionForHR( hr );
					{
						Debug.WriteLine( filterInfo.achName );		// �t�B���^���\���B
						if( filterInfo.pGraph != null )
							C�ϊ�.tCOM�I�u�W�F�N�g���������( ref filterInfo.pGraph );
					}

					IEnumPins ePins;
					hr = filters[ 0 ].EnumPins( out ePins );
					DsError.ThrowExceptionForHR( hr );
					{
						var pins = new IPin[ 1 ];
						while( ePins.Next( 1, pins, IntPtr.Zero ) == CWin32.S_OK )
						{
							PinInfo pinInfo;
							hr = pins[ 0 ].QueryPinInfo( out pinInfo );
							DsError.ThrowExceptionForHR( hr );
							{
								Debug.Write( "  " + pinInfo.name );	// �s�����\���B
								Debug.Write( ( pinInfo.dir == PinDirection.Input ) ? " �� " : " �� " );

								IPin connectPin;
								hr = pins[ 0 ].ConnectedTo( out connectPin );
								if( hr != CWin32.S_OK )
									Debug.WriteLine( "(���ڑ�)" );
								else
								{
									DsError.ThrowExceptionForHR( hr );

									PinInfo connectPinInfo;
									hr = connectPin.QueryPinInfo( out connectPinInfo );
									DsError.ThrowExceptionForHR( hr );
									{
										FilterInfo connectFilterInfo;
										hr = connectPinInfo.filter.QueryFilterInfo( out connectFilterInfo );
										DsError.ThrowExceptionForHR( hr );
										{
											Debug.Write( "[" + connectFilterInfo.achName + "]." );	// �ڑ���t�B���^��

											if( connectFilterInfo.pGraph != null )
												C�ϊ�.tCOM�I�u�W�F�N�g���������( ref connectFilterInfo.pGraph );
										}

										Debug.WriteLine( connectPinInfo.name );		// �ڑ���s����
										if( connectPinInfo.filter != null )
											C�ϊ�.tCOM�I�u�W�F�N�g���������( ref connectPinInfo.filter );
										DsUtils.FreePinInfo( connectPinInfo );
									}
									C�ϊ�.tCOM�I�u�W�F�N�g���������( ref connectPin );
								}
								if( pinInfo.filter != null )
									C�ϊ�.tCOM�I�u�W�F�N�g���������( ref pinInfo.filter );
								DsUtils.FreePinInfo( pinInfo );
							}
							C�ϊ�.tCOM�I�u�W�F�N�g���������( ref pins[ 0 ] );
						}
					}
					C�ϊ�.tCOM�I�u�W�F�N�g���������( ref ePins );

					C�ϊ�.tCOM�I�u�W�F�N�g���������( ref filters[ 0 ] );
				}
			}
			C�ϊ�.tCOM�I�u�W�F�N�g���������( ref eFilters );

			Debug.Flush();
		}
		public static void t�I�[�f�B�I�����_����Null�����_���ɕς��ăt�H�[�}�b�g���擾����( IGraphBuilder graphBuilder, out WaveFormat wfx, out byte[] wfx�g���f�[�^ )
		{
			int hr = 0;

			IBaseFilter audioRenderer = null;
			IPin rendererInputPin = null;
			IPin rendererConnectedOutputPin = null;
			IBaseFilter nullRenderer = null;
			IPin nullRendererInputPin = null;
			wfx = null;
			wfx�g���f�[�^ = new byte[ 0 ];

			try
			{
				// audioRenderer ��T���B

				audioRenderer = CDirectShow.t�I�[�f�B�I�����_����T���ĕԂ�( graphBuilder );
				if( audioRenderer == null )
					return;		// �Ȃ�����

				#region [ ���ʃ[���ň�x�Đ�����B�i�I�[�f�B�I�����_���̓��̓s��MediaType���A�ڑ����Ƃ͈قȂ�u���������́v�ɕς��\�������邽�߁B�j]
				//-----------------
				{
					// �����ɗ������_�ŁA�O���t�̃r�f�I�����_���͖������iNullRenderer�ւ̒u���⏜���Ȃǁj���Ă������ƁB
					// �����Ȃ��ƁAStopWhenReady() ���Ɉ�u���� Active�E�B���h�E���\������Ă��܂��B

					var mediaCtrl = (IMediaControl) graphBuilder;
					var basicAudio = (IBasicAudio) graphBuilder;
					
					basicAudio.put_Volume( -10000 );	// �ŏ�����
					

					// �O���t���Đ����Ă����~�߂�B�iPaused �� Stopped �֑J�ڂ���j
					
					mediaCtrl.StopWhenReady();

		
					// �O���t�� Stopped �ɑJ�ڊ�������܂ő҂B�iStopWhenReady() �̓O���t�� Stopped �ɂȂ�̂�҂����ɋA���Ă���B�j

					FilterState fs = FilterState.Paused;
					hr = CWin32.S_FALSE;
					while( fs != FilterState.Stopped || hr != CWin32.S_OK )
						hr = mediaCtrl.GetState( 10, out fs );
					

					// �I�������B

					basicAudio.put_Volume( 0 );			// �ő剹��
					
					basicAudio = null;
					mediaCtrl = null;
				}
				//-----------------
				#endregion

				// audioRenderer �̓��̓s����T���B

				rendererInputPin = t�ŏ��̓��̓s����T���ĕԂ�( audioRenderer );
				if( rendererInputPin == null )
					return;


				// WAVE�t�H�[�}�b�g���擾���Awfx �����֊i�[����B

				var type = new AMMediaType();
				hr = rendererInputPin.ConnectionMediaType( type );
				DsError.ThrowExceptionForHR( hr );
				try
				{
					wfx = new WaveFormat();

					#region [ type.formatPtr ���� wfx �ɁA�g���̈�������f�[�^���R�s�[����B]
					//-----------------
					var wfxTemp = new WaveFormatEx();	// SlimDX.Multimedia.WaveFormat �� Marshal.PtrToStructure() �Ŏg���Ȃ��̂ŁA���ꂪ�g���� DirectShowLib.WaveFormatEx ����Ď擾����B�i�ʓ|�c�j
					Marshal.PtrToStructure( type.formatPtr, (object) wfxTemp );
					wfx.AverageBytesPerSecond = wfxTemp.nAvgBytesPerSec;
					wfx.BitsPerSample = wfxTemp.wBitsPerSample;
					wfx.BlockAlignment = wfxTemp.nBlockAlign;
					wfx.Channels = wfxTemp.nChannels;
					wfx.FormatTag = (WaveFormatTag) ( (ushort) wfxTemp.wFormatTag );	// DirectShowLib.WaveFormatEx.wFormatTag �� short �����ASlimDX.Mulrimedia.WaveFormat.FormatTag �� ushort �ł���B�i�ʓ|�c�j
					wfx.SamplesPerSecond = wfxTemp.nSamplesPerSec;
					//-----------------
					#endregion
					#region [ �g���̈悪���݂���Ȃ炻��� wfx�g���f�[�^ �Ɋi�[����B ]
					//-----------------
					int nWaveFormatEx�{�̃T�C�Y = 16 + 2; // sizeof( WAVEFORMAT ) + sizof( WAVEFORMATEX.cbSize )
					int n�͂ݏo���T�C�Ybyte = type.formatSize - nWaveFormatEx�{�̃T�C�Y;

					if( n�͂ݏo���T�C�Ybyte > 0 )
					{
						wfx�g���f�[�^ = new byte[ n�͂ݏo���T�C�Ybyte ];
						var hGC = GCHandle.Alloc( wfx�g���f�[�^, GCHandleType.Pinned );	// �����Ȃ�[
						unsafe
						{
							byte* src = (byte*) type.formatPtr.ToPointer();
							byte* dst = (byte*) hGC.AddrOfPinnedObject().ToPointer();
							CWin32.CopyMemory( dst, src + nWaveFormatEx�{�̃T�C�Y, (uint) n�͂ݏo���T�C�Ybyte );
						}
						hGC.Free();
					}
					//-----------------
					#endregion
				}
				finally
				{
					if( type != null )
						DsUtils.FreeAMMediaType( type );
				}


				// audioRenderer �ɂȂ���o�̓s����T���B

				hr = rendererInputPin.ConnectedTo( out rendererConnectedOutputPin );
				DsError.ThrowExceptionForHR( hr );


				// audioRenderer ���O���t����ؒf����B

				rendererInputPin.Disconnect();
				rendererConnectedOutputPin.Disconnect();


				// audioRenderer ���O���t���珜������B

				hr = graphBuilder.RemoveFilter( audioRenderer );
				DsError.ThrowExceptionForHR( hr );


				// nullRenderer ���쐬���A�O���t�ɒǉ�����B

				nullRenderer = (IBaseFilter) new NullRenderer();
				hr = graphBuilder.AddFilter( nullRenderer, "Audio Null Renderer" );
				DsError.ThrowExceptionForHR( hr );


				// nullRenderer �̓��̓s����T���B

				hr = nullRenderer.FindPin( "In", out nullRendererInputPin );
				DsError.ThrowExceptionForHR( hr );


				// nullRenderer ���O���t�ɐڑ�����B

				hr = rendererConnectedOutputPin.Connect( nullRendererInputPin, null );
				DsError.ThrowExceptionForHR( hr );
			}
			finally
			{
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref nullRendererInputPin );
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref nullRenderer );
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref rendererConnectedOutputPin );
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref rendererInputPin );
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref audioRenderer );
			}
		}
		public static void t�r�f�I�����_�����O���t���珜������( IGraphBuilder graphBuilder )
		{
			int hr = 0;

			IBaseFilter videoRenderer = null;
			IPin renderInputPin = null;
			IPin connectedOutputPin = null;

			try
			{
				// videoRenderer ��T���B
				
				CDirectShow.t�r�f�I�����_���Ƃ��̓��̓s����T���ĕԂ�( graphBuilder, out videoRenderer, out renderInputPin );
				if( videoRenderer == null || renderInputPin == null )
					return;		// �Ȃ�����

				#region [ renderInputPin �֐ڑ����Ă���O�i�̏o�̓s�� connectedOutputPin ��T���B ]
				//-----------------
				renderInputPin.ConnectedTo( out connectedOutputPin );
				//-----------------
				#endregion

				if( connectedOutputPin == null )
					return;		// �Ȃ�����


				// �O�i�̏o�̓s���ƃr�f�I�����_���̓��̓s����ؒf����B�o��������ؒf���Ȃ��ƃO���t����؂藣����Ȃ��̂Œ��ӁB

				renderInputPin.Disconnect();
				connectedOutputPin.Disconnect();


				// �r�f�I�����_�����O���t���珜���B

				graphBuilder.RemoveFilter( videoRenderer );
			}
			finally
			{
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref connectedOutputPin );
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref renderInputPin );
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref videoRenderer );
			}
		}

		private static IPin t�ŏ��̓��̓s����T���ĕԂ�( IBaseFilter baseFilter )
		{
			int hr = 0;

			IPin firstInputPin = null;

			IEnumPins ePins;
			hr = baseFilter.EnumPins( out ePins );
			DsError.ThrowExceptionForHR( hr );
			try
			{
				var pins = new IPin[ 1 ];
				while( ePins.Next( 1, pins, IntPtr.Zero ) == CWin32.S_OK )
				{
					PinInfo pinfo = new PinInfo() { filter = null };
					try
					{
						hr = pins[ 0 ].QueryPinInfo( out pinfo );
						DsError.ThrowExceptionForHR( hr );

						if( pinfo.dir == PinDirection.Input )
						{
							firstInputPin = pins[ 0 ];
							break;
						}
					}
					finally
					{
						if( pinfo.filter != null )
							C�ϊ�.tCOM�I�u�W�F�N�g���������( ref pinfo.filter );
						DsUtils.FreePinInfo( pinfo );

						if( firstInputPin == null )
							C�ϊ�.tCOM�I�u�W�F�N�g���������( ref pins[ 0 ] );
					}
				}
			}
			finally
			{
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref ePins );
			}

			return firstInputPin;
		}
		private static void t�r�f�I�����_���Ƃ��̓��̓s����T���ĕԂ�( IFilterGraph graph, out IBaseFilter videoRenderer, out IPin inputPin )
		{
			int hr = 0;
			string str�t�B���^�� = null;
			string str�s��ID = null;


			// �r�f�I�����_���Ɠ��̓s����T���A���̃t�B���^���ƃs��ID���T����B

			IEnumFilters eFilters;
			hr = graph.EnumFilters( out eFilters );
			DsError.ThrowExceptionForHR( hr );
			try
			{
				var filters = new IBaseFilter[ 1 ];
				while( eFilters.Next( 1, filters, IntPtr.Zero ) == CWin32.S_OK )
				{
					try
					{
						#region [ �o�̓s�����Ȃ��i�����_���ł���j���Ƃ��m�F����B]
						//-----------------
						IEnumPins ePins;
						bool b�o�̓s�������� = false;

						hr = filters[ 0 ].EnumPins( out ePins );
						DsError.ThrowExceptionForHR( hr );
						try
						{
							var pins = new IPin[ 1 ];
							while( ePins.Next( 1, pins, IntPtr.Zero ) == CWin32.S_OK )
							{
								try
								{
									if( b�o�̓s�������� )
										continue;

									PinDirection dir;
									hr = pins[ 0 ].QueryDirection( out dir );
									DsError.ThrowExceptionForHR( hr );
									if( dir == PinDirection.Output )
										b�o�̓s�������� = true;
								}
								finally
								{
									C�ϊ�.tCOM�I�u�W�F�N�g���������( ref pins[ 0 ] );
								}
							}
						}
						finally
						{
							C�ϊ�.tCOM�I�u�W�F�N�g���������( ref ePins );
						}

						if( b�o�̓s�������� )
							continue;	// ���̃t�B���^��

						//-----------------
						#endregion
						#region [ �ڑ����̓��̓s���� MEDIATYPE_Video �ɑΉ����Ă�����A�t�B���^���ƃs��ID���擾����B]
						//-----------------
						hr = filters[ 0 ].EnumPins( out ePins );
						DsError.ThrowExceptionForHR( hr );
						try
						{
							var pins = new IPin[ 1 ];
							while( ePins.Next( 1, pins, IntPtr.Zero ) == CWin32.S_OK )
							{
								try
								{
									if( !string.IsNullOrEmpty( str�t�B���^�� ) )
										continue;

									var mediaType = new AMMediaType();

									#region [ ���ݐڑ����� MediaType ���擾�B�Ȃ����ĂȂ���Ύ��̃s���ցB]
									//-----------------
									hr = pins[ 0 ].ConnectionMediaType( mediaType );
									if( hr == CWin32.VFW_E_NOT_CONNECTED )
										continue;	// �Ȃ����ĂȂ�
									DsError.ThrowExceptionForHR( hr );
									//-----------------
									#endregion

									try
									{
										if( mediaType.majorType.Equals( MediaType.Video ) )
										{
											#region [ �t�B���^���擾�I]
											//-----------------
											FilterInfo filterInfo;
											hr = filters[ 0 ].QueryFilterInfo( out filterInfo );
											DsError.ThrowExceptionForHR( hr );
											str�t�B���^�� = filterInfo.achName;
											C�ϊ�.tCOM�I�u�W�F�N�g���������( ref filterInfo.pGraph );
											//-----------------
											#endregion
											#region [ �s��ID�擾�I]
											//-----------------
											hr = pins[ 0 ].QueryId( out str�s��ID );
											DsError.ThrowExceptionForHR( hr );
											//-----------------
											#endregion

											continue;	// ���̃s���ցB
										}
									}
									finally
									{
										DsUtils.FreeAMMediaType( mediaType );
									}
								}
								finally
								{
									C�ϊ�.tCOM�I�u�W�F�N�g���������( ref pins[ 0 ] );
								}
							}
						}
						finally
						{
							C�ϊ�.tCOM�I�u�W�F�N�g���������( ref ePins );
						}

						//-----------------
						#endregion
					}
					finally
					{
						C�ϊ�.tCOM�I�u�W�F�N�g���������( ref filters[ 0 ] );
					}
				}
			}
			finally
			{
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref eFilters );
			}


			// ���߂ăt�B���^���ƃs��ID���炱���̃C���^�[�t�F�[�X���擾���A�߂�l�Ƃ��ĕԂ��B

			videoRenderer = null;
			inputPin = null;

			if( !string.IsNullOrEmpty( str�t�B���^�� ) )
			{
				hr = graph.FindFilterByName( str�t�B���^��, out videoRenderer );
				DsError.ThrowExceptionForHR( hr );

				hr = videoRenderer.FindPin( str�s��ID, out inputPin );
				DsError.ThrowExceptionForHR( hr );
			}
		}
		private static IBaseFilter t�I�[�f�B�I�����_����T���ĕԂ�( IFilterGraph graph )
		{
			int hr = 0;
			IBaseFilter audioRenderer = null;

			IEnumFilters eFilters;
			hr = graph.EnumFilters( out eFilters );
			DsError.ThrowExceptionForHR( hr );
			try
			{
				var filters = new IBaseFilter[ 1 ];
				while( eFilters.Next( 1, filters, IntPtr.Zero ) == CWin32.S_OK )
				{
					if( ( filters[ 0 ] as IAMAudioRendererStats ) != null )
					{
						audioRenderer = filters[ 0 ];
						break;
					}

					C�ϊ�.tCOM�I�u�W�F�N�g���������( ref filters[ 0 ] );
				}
			}
			finally
			{
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref eFilters );
			}
			return audioRenderer;
		}


		#region [ �ÓI�C���X�^���X�Ǘ� ]
		//-----------------
		public const int n�C���X�^���XID�̍ő吔 = 100;
		protected static Dictionary<int, CDirectShow> dic�C���X�^���X = new Dictionary<int, CDirectShow>();	// <�C���X�^���XID, ����ID�����C���X�^���X>

		public static CDirectShow t�C���X�^���X��Ԃ�( int n�C���X�^���XID )
		{
			if( CDirectShow.dic�C���X�^���X.ContainsKey( n�C���X�^���XID ) )
				return CDirectShow.dic�C���X�^���X[ n�C���X�^���XID ];

			return null;
		}
		protected static void t�C���X�^���X��o�^����( CDirectShow ds )
		{
			for( int i = 1; i < CDirectShow.n�C���X�^���XID�̍ő吔; i++ )
			{
				if( !CDirectShow.dic�C���X�^���X.ContainsKey( i ) )		// �󂢂Ă���ԍ����g���B
				{
					ds.n�C���X�^���XID = i;
					CDirectShow.dic�C���X�^���X.Add( i, ds );
					break;
				}
			}
		}
		protected static void t�C���X�^���X���������( int n�C���X�^���XID )
		{
			if( CDirectShow.dic�C���X�^���X.ContainsKey( n�C���X�^���XID ) )
				CDirectShow.dic�C���X�^���X.Remove( n�C���X�^���XID );
		}
		//-----------------
		#endregion

		#region [ Dispose-Finalize �p�^�[������ ]
		//-----------------
		public virtual void Dispose()
		{
			this.Dispose( true );
			GC.SuppressFinalize( this );	// ������ Dispose ���ꂽ�̂ŁA�t�@�C�i���C�Y�s�v�ł��邱�Ƃ� CLR �ɓ`����B
		}
		protected virtual void Dispose( bool bManaged���\�[�X��������� )
		{
			if( bManaged���\�[�X��������� )
			{
				#region [ ROT����������B]
				//-----------------
#if DEBUG
					C�ϊ�.tDispose����( ref this.rot );
#endif
				//-----------------
				#endregion
				
				CDirectShow.t�C���X�^���X���������( this.n�C���X�^���XID );
			}

			#region [ �C���^�[�t�F�[�X�Q�Ƃ��Ȃ����ACOM�I�u�W�F�N�g���������B ]
			//-----------------
			if( this.ip != IntPtr.Zero )
			{
				Marshal.FreeCoTaskMem( this.ip );
				this.ip = IntPtr.Zero;
			}

			if( this.MediaCtrl != null )
			{
				this.MediaCtrl.Stop();
				this.MediaCtrl = null;
			}

			if( this.MediaEventEx != null )
			{
				this.MediaEventEx.SetNotifyWindow( IntPtr.Zero, 0, IntPtr.Zero );
				this.MediaEventEx = null;
			}

			if( this.MediaSeeking != null )
				this.MediaSeeking = null;

			if( this.BasicAudio != null )
				this.BasicAudio = null;

			C�ϊ�.tCOM�I�u�W�F�N�g���������( ref this.nullRenderer );
			C�ϊ�.tCOM�I�u�W�F�N�g���������( ref this.memoryRenderer );
			C�ϊ�.tCOM�I�u�W�F�N�g���������( ref this.memoryRendererObject );
			C�ϊ�.tCOM�I�u�W�F�N�g���������( ref this.graphBuilder );
			//-----------------
			#endregion

			C�ϊ�.t���S�ȃK�x�[�W�R���N�V���������{����();
		}
        ~CDirectShow()
		{
			// �t�@�C�i���C�U���Ă΂ꂽ�Ƃ������Ƃ́ADispose() ����Ȃ������Ƃ������ƁB
			// ���̏ꍇ�AManaged ���\�[�X�͐�Ƀt�@�C�i���C�Y����Ă���\��������̂ŁAUnmamaed ���\�[�X�݂̂��������B
			
			this.Dispose( false );
        }
		//-----------------
		#endregion

		#region [ protected ]
		//-----------------
		protected MemoryRenderer memoryRendererObject = null;
		protected IMemoryRenderer memoryRenderer = null;
		protected IBaseFilter nullRenderer = null;
		protected int n�Đ��ꎞ��~�Ăяo���̗ݐω� = 0;
		//-----------------
		#endregion

		#region [ private ]
		//-----------------
		private int _n���� = 100;
#if DEBUG
		private DsROTEntry rot = null;
#endif

		// �\�Ȑ��̃X���b�h���g�p���ĉ摜��]������B�傫���摜�قǗL���B��������ƃv�[�����̃X���b�h���󂭂܂ő҂������̂Œ��ӁB
		private static int n����x = 0;	// 0 �̏ꍇ�A�ŏ��̐������ɕ���x�����肷��B

		private DG���C���`��[] dg���C���`��ARGB32;
		private DG���C���`��[] dg���C���`��XRGB32;
		private unsafe delegate void DG���C���`��( int n );
		private unsafe byte* ptrSnap = null;
		private unsafe UInt32** ptrTexture = null;

		private unsafe void t���C���`��XRGB32( int n )
		{
			// Snap �� RGB32�ATexture�� X8R8G8B8

			UInt32* ptrTexture = this.ptrTexture[ n ];
			for( int y = n; y < this.n����px; y += CDirectShow.n����x )
			{
				byte* ptrPixel = ptrSnap + ( ( ( this.n����px - y ) - 1 ) * this.n�X�L�������C����byte );

				// �A���t�@�����Ȃ̂ňꊇ�R�s�[�BCopyMemory() �͎��O�Ń��[�v�W�J������������B
				CWin32.CopyMemory( (void*) ptrTexture, (void*) ptrPixel, (uint) ( this.n��px * 4 ) );

				ptrTexture += this.n��px * CDirectShow.n����x;
			}
		}
		private unsafe void t���C���`��ARGB32( int n )
		{
			// Snap �� RGB32�ATexture�� A8R8G8B8

			UInt32* ptrTexture = this.ptrTexture[ n ];
			for( int y = n; y < this.n����px; y += CDirectShow.n����x )
			{
				UInt32* ptrPixel = (UInt32*) ( ptrSnap + ( ( ( this.n����px - y ) - 1 ) * this.n�X�L�������C����byte ) );

				//for( int x = 0; x < this.n��px; x++ )
				//	*( ptrTexture + x ) = 0xFF000000 | *ptrPixel++;
				//			�����[�v�W�J�ɂ�荂�����B160fps �̋Ȃ� 200fps �܂ŏオ�����B

				if( this.n��px == 0 ) goto LEXIT;
				UInt32* pt = ptrTexture;
				UInt32 nAlpha = 0xFF000000;
				int d = ( this.n��px % 32 );

				switch( d )
				{
					case 1: goto L031;
					case 2: goto L030;
					case 3: goto L029;
					case 4: goto L028;
					case 5: goto L027;
					case 6: goto L026;
					case 7: goto L025;
					case 8: goto L024;
					case 9: goto L023;
					case 10: goto L022;
					case 11: goto L021;
					case 12: goto L020;
					case 13: goto L019;
					case 14: goto L018;
					case 15: goto L017;
					case 16: goto L016;
					case 17: goto L015;
					case 18: goto L014;
					case 19: goto L013;
					case 20: goto L012;
					case 21: goto L011;
					case 22: goto L010;
					case 23: goto L009;
					case 24: goto L008;
					case 25: goto L007;
					case 26: goto L006;
					case 27: goto L005;
					case 28: goto L004;
					case 29: goto L003;
					case 30: goto L002;
					case 31: goto L001;
				}

			L000: *pt++ = nAlpha | *ptrPixel++;
			L001: *pt++ = nAlpha | *ptrPixel++;
			L002: *pt++ = nAlpha | *ptrPixel++;
			L003: *pt++ = nAlpha | *ptrPixel++;
			L004: *pt++ = nAlpha | *ptrPixel++;
			L005: *pt++ = nAlpha | *ptrPixel++;
			L006: *pt++ = nAlpha | *ptrPixel++;
			L007: *pt++ = nAlpha | *ptrPixel++;
			L008: *pt++ = nAlpha | *ptrPixel++;
			L009: *pt++ = nAlpha | *ptrPixel++;
			L010: *pt++ = nAlpha | *ptrPixel++;
			L011: *pt++ = nAlpha | *ptrPixel++;
			L012: *pt++ = nAlpha | *ptrPixel++;
			L013: *pt++ = nAlpha | *ptrPixel++;
			L014: *pt++ = nAlpha | *ptrPixel++;
			L015: *pt++ = nAlpha | *ptrPixel++;
			L016: *pt++ = nAlpha | *ptrPixel++;
			L017: *pt++ = nAlpha | *ptrPixel++;
			L018: *pt++ = nAlpha | *ptrPixel++;
			L019: *pt++ = nAlpha | *ptrPixel++;
			L020: *pt++ = nAlpha | *ptrPixel++;
			L021: *pt++ = nAlpha | *ptrPixel++;
			L022: *pt++ = nAlpha | *ptrPixel++;
			L023: *pt++ = nAlpha | *ptrPixel++;
			L024: *pt++ = nAlpha | *ptrPixel++;
			L025: *pt++ = nAlpha | *ptrPixel++;
			L026: *pt++ = nAlpha | *ptrPixel++;
			L027: *pt++ = nAlpha | *ptrPixel++;
			L028: *pt++ = nAlpha | *ptrPixel++;
			L029: *pt++ = nAlpha | *ptrPixel++;
			L030: *pt++ = nAlpha | *ptrPixel++;
			L031: *pt++ = nAlpha | *ptrPixel++;
				if( ( pt - ptrTexture ) < this.n��px ) goto L000;

			LEXIT:
				ptrTexture += this.n��px * CDirectShow.n����x;
			}
		}
		//-----------------
		#endregion
	}
}
