/*
* Copyright (c) 2007-2012 SlimDX Group
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

#include "Enums.h"
#include "Resource10.h"
#include "ImageLoadInformation.h"

namespace SlimDX
{
	ref class DataStream;

	namespace Direct3D10
	{
		ref class Device;
		value class Texture1DDescription;
		
		public ref class Texture1D : public Resource
		{
			COMOBJECT(ID3D10Texture1D, Texture1D);
		
		private:
			static ID3D10Texture1D* Build( SlimDX::Direct3D10::Device^ device, Texture1DDescription description, D3D10_SUBRESOURCE_DATA* data ); 
		
		public:
			/// <summary>
			/// Gets the texture description.
			/// </summary>
			property Texture1DDescription Description
			{
				Texture1DDescription get();
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture1D"/> class.
			/// </summary>
			/// <param name="device">The device to associate the texture with.</param>
			/// <param name="description">The description of the texture.</param>
			Texture1D( SlimDX::Direct3D10::Device^ device, Texture1DDescription description );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture1D"/> class.
			/// </summary>
			/// <param name="device">The device to associate the texture with.</param>
			/// <param name="description">The description of the texture.</param>
			/// <param name="data">The initial texture data.</param>
			Texture1D( SlimDX::Direct3D10::Device^ device, Texture1DDescription description, DataStream^ data );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Texture1D"/> class.
			/// </summary>
			/// <param name="device">The device to associate the texture with.</param>
			/// <param name="description">The description of the texture.</param>
			/// <param name="data">An array of initial texture data for each subresource.</param>
			Texture1D( SlimDX::Direct3D10::Device^ device, Texture1DDescription description, array<DataStream^>^ data );
			
			/// <summary>
			/// Maps the texture, providing CPU access to its contents.
			/// </summary>
			/// <param name="mipSlice">A zero-based index into an array of subtextures; 0 indicates the first, most detailed subtexture (or mipmap level).</param>
			/// <param name="mode">The IO operations to enable on the CPU.</param>
			/// <param name="flags">Flags indicating how the CPU should respond when the GPU is busy.</param>
			/// <returns>A data stream containing the mapped data. This data stream is invalidated
			/// when the buffer is unmapped.</returns>
			DataStream^ Map( int mipSlice, MapMode mode, MapFlags flags );

			/// <summary>
			/// Maps the texture, providing CPU access to its contents.
			/// </summary>
			/// <param name="mipSlice">A zero-based index into an array of subtextures; 0 indicates the first, most detailed subtexture (or mipmap level).</param>
			/// <param name="arraySlice">The zero-based index of the first texture to use (in an array of textures).</param>
			/// <param name="mode">The IO operations to enable on the CPU.</param>
			/// <param name="flags">Flags indicating how the CPU should respond when the GPU is busy.</param>
			/// <returns>A data stream containing the mapped data. This data stream is invalidated
			/// when the buffer is unmapped.</returns>
			DataStream^ Map( int mipSlice, int arraySlice, MapMode mode, MapFlags flags );
			
			/// <summary>
			/// Unmaps the texture.
			/// </summary>
			/// <param name="subresource">The subresource to unmap.</param>
			void Unmap( int subresource );
			
			static Texture1D^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName );
			static Texture1D^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, ImageLoadInformation loadInfo );
			static Texture1D^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory );
			static Texture1D^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, ImageLoadInformation loadInfo );
			static Texture1D^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes );
			static Texture1D^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, int sizeInBytes, ImageLoadInformation loadInfo );

			static Result ToFile( Texture1D^ texture, ImageFileFormat format, System::String^ fileName );
			static Result ToStream( Texture1D^ texture, ImageFileFormat format, System::IO::Stream^ stream );
		};
	}
};