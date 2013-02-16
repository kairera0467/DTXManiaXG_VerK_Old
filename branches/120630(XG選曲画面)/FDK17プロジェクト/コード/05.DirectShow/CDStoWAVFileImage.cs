using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DirectShowLib;
using SlimDX.Multimedia;

namespace FDK
{
	public class CDStoWAVFileImage
	{
		/// <summary>
		/// <para>�w�肳�ꂽ����t�@�C�����特���݂̂��G���R�[�h���AWAV�t�@�C���C���[�W���쐬���ĕԂ��B</para>
		/// </summary>
		public static void t�ϊ�( string fileName, out byte[] wavFileImage )
		{
			int hr = 0;

			IGraphBuilder graphBuilder = null;

			try
			{
				graphBuilder = (IGraphBuilder) new FilterGraph();

				#region [ �I�[�f�B�I�p�T���v���O���o�̍쐬�ƒǉ��B]
				//-----------------
				ISampleGrabber sampleGrabber = null;
				try
				{
					sampleGrabber = (ISampleGrabber) new SampleGrabber();


					// �T���v���O���o�̃��f�B�A�^�C�v�̐ݒ�B

					var mediaType = new AMMediaType() {
						majorType = MediaType.Audio,
						subType = MediaSubType.PCM,
						formatType = FormatType.WaveEx,
					};
					try
					{
						hr = sampleGrabber.SetMediaType( mediaType );
						DsError.ThrowExceptionForHR( hr );
					}
					finally
					{
						if( mediaType != null )
							DsUtils.FreeAMMediaType( mediaType );
					}


					// �T���v���O���o�̃o�b�t�@�����O��L���ɂ���B

					hr = sampleGrabber.SetBufferSamples( true );
					DsError.ThrowExceptionForHR( hr );


					// �T���v���O���o�ɃR�[���o�b�N��ǉ�����B

					sampleGrabberProc = new CSampleGrabberCallBack();
					hr = sampleGrabber.SetCallback( sampleGrabberProc, 1 );	// 1:�R�[���o�b�N�� BufferCB() ���\�b�h�̕����Ăяo���B


					// �T���v���O���o���O���t�ɒǉ�����B

					hr = graphBuilder.AddFilter( (IBaseFilter) sampleGrabber, "SampleGrabber for Audio/PCM" );
					DsError.ThrowExceptionForHR( hr );
				}
				finally
				{
					C�ϊ�.tCOM�I�u�W�F�N�g���������( ref sampleGrabber );
				}
				//-----------------
				#endregion

				var e = new DirectShowLib.DsROTEntry( graphBuilder );

				// fileName ����O���t�����������B

				hr = graphBuilder.RenderFile( fileName, null );	// IMediaControl.RenderFile() �͐�������Ȃ�
				DsError.ThrowExceptionForHR( hr );


				// �r�f�I�����_���������B

				CDirectShow.t�r�f�I�����_�����O���t���珜������( graphBuilder );		// �I�[�f�B�I�����_����Null�ɕς�����O�Ɏ��s���邱�ƁB�iCDirectShow.t�I�[�f�B�I�����_����Null�����_���ɕς��ăt�H�[�}�b�g���擾����() �̒��ň�x�Đ�����̂ŁA���̂Ƃ���Active�E�B���h�E���\������Ă��܂����߁B�j
	

				// �I�[�f�B�I�����_���� NullRenderer �ɒu���B

				WaveFormat wfx;
				byte[] wfx�g���̈�;
				CDirectShow.t�I�[�f�B�I�����_����Null�����_���ɕς��ăt�H�[�}�b�g���擾����( graphBuilder, out wfx, out wfx�g���̈� );


				// ��N���b�N�� NULL�i�ō����j�ɐݒ肷��B

				IMediaFilter mediaFilter = graphBuilder as IMediaFilter;
				mediaFilter.SetSyncSource( null );
				mediaFilter = null;


				// �������X�g���[���Ƀf�R�[�h�f�[�^���o�͂���B

				sampleGrabberProc.MemoryStream = new MemoryStream();	// CDirectShow.t�I�[�f�B�I�����_����Null�����_���ɕς��ăt�H�[�}�b�g���擾����() �ň�x�Đ����Ă���̂ŁA�X�g���[�����N���A����B
				var ms = sampleGrabberProc.MemoryStream;
				var bw = new BinaryWriter( ms );
				bw.Write( new byte[] { 0x52, 0x49, 0x46, 0x46 } );		// 'RIFF'
				bw.Write( (UInt32) 0 );									// �t�@�C���T�C�Y - 8 [byte]�G���͕s���Ȃ̂Ō�ŏ㏑������B
				bw.Write( new byte[] { 0x57, 0x41, 0x56, 0x45 } );		// 'WAVE'
				bw.Write( new byte[] { 0x66, 0x6D, 0x74, 0x20 } );		// 'fmt '
				bw.Write( (UInt32) ( 16 + ( ( wfx�g���̈�.Length > 0 ) ? ( 2/*sizeof(WAVEFORMATEX.cbSize)*/ + wfx�g���̈�.Length ) : 0 ) ) );	// fmt�`�����N�̃T�C�Y[byte]
				bw.Write( (UInt16) wfx.FormatTag );						// �t�H�[�}�b�gID�i���j�APCM�Ȃ�1�j
				bw.Write( (UInt16) wfx.Channels );						// �`�����l����
				bw.Write( (UInt32) wfx.SamplesPerSecond );				// �T���v�����O���[�g
				bw.Write( (UInt32) wfx.AverageBytesPerSecond );			// �f�[�^���x
				bw.Write( (UInt16) wfx.BlockAlignment );				// �u���b�N�T�C�Y
				bw.Write( (UInt16) wfx.BitsPerSample );					// �T���v��������̃r�b�g��
				if( wfx�g���̈�.Length > 0 )
				{
					bw.Write( (UInt16) wfx�g���̈�.Length );			// �g���̈�̃T�C�Y[byte]
					bw.Write( wfx�g���̈� );							// �g���f�[�^
				}
				bw.Write( new byte[] { 0x64, 0x61, 0x74, 0x61 } );		// 'data'
				int nDATA�`�����N�T�C�Y�ʒu = (int) ms.Position;
				bw.Write( (UInt32) 0 );									// data�`�����N�̃T�C�Y[byte]�G���͕s���Ȃ̂Ō�ŏ㏑������B

				#region [ �Đ����J�n���A�I����҂B- �Đ����AsampleGrabberProc.MemoryStream �� PCM �f�[�^���~�ς���Ă����B]
				//-----------------
				IMediaControl mediaControl = graphBuilder as IMediaControl;
				mediaControl.Run();						// �Đ��J�n

				IMediaEvent mediaEvent = graphBuilder as IMediaEvent;
				EventCode eventCode;
				hr = mediaEvent.WaitForCompletion( -1, out eventCode );
				DsError.ThrowExceptionForHR( hr );
				if( eventCode != EventCode.Complete )
					throw new Exception( "�Đ��҂��Ɏ��s���܂����B" );

				mediaControl.Stop();
				mediaEvent = null;
				mediaControl = null;
				//-----------------
				#endregion

				bw.Seek( 4, SeekOrigin.Begin );
				bw.Write( (UInt32) ms.Length - 8 );					// �t�@�C���T�C�Y - 8 [byte]

				bw.Seek( nDATA�`�����N�T�C�Y�ʒu, SeekOrigin.Begin );
				bw.Write( (UInt32) ms.Length - ( nDATA�`�����N�T�C�Y�ʒu + 4 ) );	// data�`�����N�T�C�Y [byte]


				// �o�͂��̂Q���쐬�B

				wavFileImage = ms.ToArray();


				// �I�������B

				bw.Close();
				sampleGrabberProc.Dispose();	// ms.Close()
			}
			finally
			{
				C�ϊ�.tCOM�I�u�W�F�N�g���������( ref graphBuilder );
			}
		}

		#region [ private ]
		//-----------------
		private class CSampleGrabberCallBack : ISampleGrabberCB, IDisposable
		{
			public MemoryStream MemoryStream = new MemoryStream();

			public int BufferCB( double SampleTime, IntPtr pBuffer, int BufferLen )
			{
				var bytes = new byte[ BufferLen ];
				Marshal.Copy( pBuffer, bytes, 0, BufferLen );		// unmanage �� manage
				this.MemoryStream.Write( bytes, 0, BufferLen );		// byte[] �� Stream
				return CWin32.S_OK;
			}
			public int SampleCB( double SampleTime, IMediaSample pSample )
			{
				throw new NotImplementedException( "��������Ă��܂���B" );
			}

			public void Dispose()
			{
				this.MemoryStream.Close();
			}
		}
		private static CSampleGrabberCallBack sampleGrabberProc = null;
		//-----------------
		#endregion
	}
}
