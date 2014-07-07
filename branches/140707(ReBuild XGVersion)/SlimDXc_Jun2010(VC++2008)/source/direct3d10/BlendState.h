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

#include "../ComObject.h"

#include "DeviceChild10.h"
#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D10
	{	
		ref class Device;
		value class BlendStateDescription;
		
		/// <summary>
		/// A state object defining the behavior of the pixel-blending stage of the output merger. 
		/// </summary>
		/// <unmanaged>ID3D10BlendState</unmanaged>
		public ref class BlendState : public DeviceChild
		{
			COMOBJECT(ID3D10BlendState, BlendState);
		
		protected:
			//no-op constructor for use by BlendState1
			BlendState() { }

		public:
			/// <summary>
			/// Gets the state's description.
			/// </summary>
			property BlendStateDescription Description
			{
				BlendStateDescription get();
			}
			
			/// <summary>
			/// Constructs a new BlendState based on the specified description. If the description
			/// is identical to that of an existing BlendState object, the existing BlendState object
			/// is returned instead of a new instance.
			/// </summary>
			/// <param name="device">The device to associate the state object with</param>
			/// <param name="description">The state description.</param>
			/// <returns>The BlendState object.</returns>
			static BlendState^ FromDescription( SlimDX::Direct3D10::Device^ device, BlendStateDescription description );
		};
	}
};
