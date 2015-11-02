/*
* Copyright (c) 2007-2010 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
#pragma once

#include "Enums.h"
#include "DeviceCollection.h"
#include "CaptureCapabilities.h"

namespace SlimDX
{
	namespace DirectSound
	{
		/// <summary>
		/// The DirectSoundCapture object is used to create sound capture buffers.
		/// </summary>
		/// <unmanaged>IDirectSoundCapture8</unmanaged>
		public ref class DirectSoundCapture : public ComObject
		{
			COMOBJECT(IDirectSoundCapture, DirectSoundCapture);

		public:
			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::DirectSound::DirectSoundCapture"/> class.
			/// </summary>
			DirectSoundCapture();

			/// <summary>
			/// Initializes a new instance of the <see cref="SlimDX::DirectSound::DirectSoundCapture"/> class.
			/// </summary>
			/// <param name="device"></param>
			/// <returns></returns>
			DirectSoundCapture( System::Guid device );

			/// <summary>
			/// Retrieves the capabilities of the capture device.
			/// </summary>
			property CaptureCapabilities Capabilities
			{
				CaptureCapabilities get();
			}

			/// <summary>
			/// Enumerates the DirectSound capture devices installed in the system.
			/// </summary>
			/// <returns>A collection of the devices found.</returns>
			static DeviceCollection^ GetDevices();
		};
	}
}