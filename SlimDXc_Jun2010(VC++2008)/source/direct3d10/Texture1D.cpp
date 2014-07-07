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
#include "stdafx.h"
#include <d3d10.h>

#include "../stack_array.h"
#include "../DataStream.h"
#include "../Utilities.h"

#include "Direct3D10Exception.h"

#include "Device10.h"
#include "Texture1D.h"
#include "Texture1DDescription.h"

using namespace System;
using namespace System::IO;

namespace SlimDX
{
namespace Direct3D10
{ 
	Texture1D::Texture1D( SlimDX::Direct3D10::Device^ device, Texture1DDescription description )
	{
		Construct( Build( device, description, 0 ) );	
	}
	
	Texture1D::Texture1D( SlimDX::Direct3D10::Device^ device, Texture1DDescription description, DataStream^ data )
	{
		if( data != nullptr )
		{
			D3D10_SUBRESOURCE_DATA initialData;
			initialData.pSysMem = data->PositionPointer;
			Construct( Build( device, description, &initialData ) );	
		}
		else 
		{
			Construct( Build( device, description, 0 ) );	
		}
	}
	
	Texture1D::Texture1D( SlimDX::Direct3D10::Device^ device, Texture1DDescription description, array<DataStream^>^ data )
	{
		if( data != nullptr )
		{
			stack_array<D3D10_SUBRESOURCE_DATA> initialData = stackalloc( D3D10_SUBRESOURCE_DATA, data->Length );
			for( size_t dataIndex = 0; dataIndex < initialData.size(); ++dataIndex ) 
				initialData[dataIndex].pSysMem = data[static_cast<int>( dataIndex )]->PositionPointer;
			
			Construct( Build( device, description, &initialData[0] ) );	
		} 
		else
		{
			Construct( Build( device, description, 0 ) );	
		}
	}
	
	ID3D10Texture1D* Texture1D::Build( SlimDX::Direct3D10::Device^ device, Texture1DDescription description, D3D10_SUBRESOURCE_DATA* data )
	{
		ID3D10Texture1D* texture = 0;
		D3D10_TEXTURE1D_DESC nativeDescription = description.CreateNativeVersion();
		
		if( RECORD_D3D10( device->InternalPointer->CreateTexture1D( &nativeDescription, data, &texture ) ).IsFailure )
			throw gcnew Direct3D10Exception( Result::Last );
		
		return texture;
	}
	
	Texture1DDescription Texture1D::Description::get()
	{
		D3D10_TEXTURE1D_DESC nativeDescription;
		InternalPointer->GetDesc( &nativeDescription );
		return Texture1DDescription( nativeDescription );
	}
	
	SlimDX::DataStream^ Texture1D::Map( int mipSlice, MapMode mode, MapFlags flags )
	{
		int subresource = D3D10CalcSubresource( mipSlice, 0, Description.MipLevels );
		int mipWidth = GetMipSize( mipSlice, Description.Width );
		int bufferSize = mipWidth * Utilities::SizeOfFormatElement( static_cast<DXGI_FORMAT>( Description.Format ) );
		
		void* mappedArray = 0;
		if( RECORD_D3D10( InternalPointer->Map( subresource, static_cast<D3D10_MAP>( mode ), static_cast<UINT>( flags ), &mappedArray ) ).IsFailure )
			return nullptr;
			
		return gcnew SlimDX::DataStream( mappedArray, bufferSize, true, true, false );
	}

	void Texture1D::Unmap( int subresource )
	{
		InternalPointer->Unmap( subresource );
	}
	
	Texture1D^ Texture1D::FromFile( SlimDX::Direct3D10::Device^ device, String^ fileName )
	{
		ID3D10Resource* resource = Resource::ConstructFromFile( device, fileName, 0 );
		if( resource == 0 )
			return nullptr;
			
		D3D10_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D10_RESOURCE_DIMENSION_TEXTURE1D )
			throw gcnew InvalidOperationException( "Could not load file as 1D texture." );

		return Texture1D::FromPointer( static_cast<ID3D10Texture1D*>( resource ) );
	}
	
	Texture1D^ Texture1D::FromMemory( SlimDX::Direct3D10::Device^ device, array<Byte>^ memory )
	{
		ID3D10Resource* resource = Resource::ConstructFromMemory( device, memory, 0 );
		if( resource == 0 )
			return nullptr;
			
		D3D10_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D10_RESOURCE_DIMENSION_TEXTURE1D )
			throw gcnew InvalidOperationException( "Could not load file as 1D texture." );

		return Texture1D::FromPointer( static_cast<ID3D10Texture1D*>( resource ) );
	}
	
	Texture1D^ Texture1D::FromStream( SlimDX::Direct3D10::Device^ device, Stream^ stream, int sizeInBytes )
	{
		ID3D10Resource* resource = Resource::ConstructFromStream( device, stream, sizeInBytes, 0 );
		if( resource == 0 )
			return nullptr;
			
		D3D10_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D10_RESOURCE_DIMENSION_TEXTURE1D )
			throw gcnew InvalidOperationException( "Could not load file as 1D texture." );

		return Texture1D::FromPointer( static_cast<ID3D10Texture1D*>( resource ) );
	}
	
	Texture1D^ Texture1D::FromFile( SlimDX::Direct3D10::Device^ device, String^ fileName, ImageLoadInformation loadInfo )
	{
		D3DX10_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D10Resource* resource = Resource::ConstructFromFile( device, fileName, &info );
		if( resource == 0 )
			return nullptr;

		D3D10_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D10_RESOURCE_DIMENSION_TEXTURE1D )
			throw gcnew InvalidOperationException( "Could not load file as 1D texture." );

		return Texture1D::FromPointer( static_cast<ID3D10Texture1D*>( resource ) );
	}

	Texture1D^ Texture1D::FromMemory( SlimDX::Direct3D10::Device^ device, array<Byte>^ memory, ImageLoadInformation loadInfo )
	{
		D3DX10_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D10Resource* resource = Resource::ConstructFromMemory( device, memory, &info );
		if( resource == 0 )
			return nullptr;

		D3D10_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D10_RESOURCE_DIMENSION_TEXTURE1D )
			throw gcnew InvalidOperationException( "Could not load file as 1D texture." );

		return Texture1D::FromPointer( static_cast<ID3D10Texture1D*>( resource ) );
	}

	Texture1D^ Texture1D::FromStream( SlimDX::Direct3D10::Device^ device, Stream^ stream, int sizeInBytes, ImageLoadInformation loadInfo )
	{
		D3DX10_IMAGE_LOAD_INFO info = loadInfo.CreateNativeVersion();
		ID3D10Resource* resource = Resource::ConstructFromStream( device, stream, sizeInBytes, &info );
		if( resource == 0 )
			return nullptr;

		D3D10_RESOURCE_DIMENSION type;
		resource->GetType( &type );
		if( type != D3D10_RESOURCE_DIMENSION_TEXTURE1D )
			throw gcnew InvalidOperationException( "Could not load file as 1D texture." );

		return Texture1D::FromPointer( static_cast<ID3D10Texture1D*>( resource ) );
	}

	Result Texture1D::ToFile( Texture1D^ texture, ImageFileFormat format, String^ fileName )
	{
		pin_ptr<const wchar_t> pinnedName = PtrToStringChars( fileName );

		HRESULT hr = D3DX10SaveTextureToFile( texture->InternalPointer, static_cast<D3DX10_IMAGE_FILE_FORMAT>( format ), pinnedName );
		return RECORD_D3D10( hr );
	}

	Result Texture1D::ToStream( Texture1D^ texture, ImageFileFormat format, Stream^ stream )
	{
		ID3D10Blob* blob = 0;
		HRESULT hr = D3DX10SaveTextureToMemory( texture->InternalPointer, static_cast<D3DX10_IMAGE_FILE_FORMAT>( format ), &blob, 0 );
		if( RECORD_D3D10( hr ).IsFailure )
			return Result::Last;
		
		// Write byte-by-byte to avoid allocating a managed byte[] that will wastefully persist.
		unsigned char* bytes = reinterpret_cast<unsigned char*>( blob->GetBufferPointer() );
		for(SIZE_T byteIndex = 0; byteIndex < blob->GetBufferSize(); ++byteIndex)
			stream->WriteByte( bytes[byteIndex] );
		
		blob->Release();
		return Result::Last;
	}
}
}
