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

#include "../stack_array.h"

#include "Buffer11.h"
#include "SamplerState11.h"
#include "ShaderResourceView11.h"
#include "UnorderedAccessView11.h"
#include "ComputeShaderWrapper11.h"
#include "ComputeShader11.h"
#include "ClassInstance11.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D11
{ 
	ComputeShaderWrapper::ComputeShaderWrapper( ID3D11DeviceContext* device )
	{
		if( device == 0 )
			throw gcnew ArgumentNullException( "deviceContext" );
		deviceContext = device;
	}

	void ComputeShaderWrapper::Set( ComputeShader^ shader )
	{
		Set( shader, nullptr );
	}

	void ComputeShaderWrapper::Set( ComputeShader^ shader, array<ClassInstance^>^ classInstances )
	{
		ID3D11ComputeShader *nativeShader = shader == nullptr ? NULL : shader->InternalPointer;
		ID3D11ClassInstance** instancePtr = NULL;
		stack_array<ID3D11ClassInstance*> instances;
		UINT count = 0;

		if( classInstances != nullptr && classInstances->Length > 0 )
		{
			instances = stack_array<ID3D11ClassInstance*>( classInstances->Length );
			instancePtr = &instances[0];
			count = classInstances->Length;

			for( int i = 0; i < classInstances->Length; i++ )
				instances[i] = classInstances[i]->InternalPointer;
		}

		deviceContext->CSSetShader( nativeShader, instancePtr, count );
	}

	ComputeShader^ ComputeShaderWrapper::Get()
	{
		return Get( nullptr );
	}

	ComputeShader^ ComputeShaderWrapper::Get( array<ClassInstance^>^ classInstances )
	{
		ID3D11ComputeShader *shader = NULL;
		ID3D11ClassInstance** instancePtr = NULL;
		stack_array<ID3D11ClassInstance*> instances;
		UINT count = 0;

		if( classInstances != nullptr && classInstances->Length > 0 )
		{
			instances = stack_array<ID3D11ClassInstance*>( classInstances->Length );
			instancePtr = &instances[0];
			count = classInstances->Length;
		}

		deviceContext->CSGetShader( &shader, instancePtr, &count );

		for( UINT i = 0; i < count; i++ )
			classInstances[i] = ClassInstance::FromPointer( instances[i] );

		return shader == NULL ? nullptr : ComputeShader::FromPointer( shader );
	}

	array<Buffer^>^ ComputeShaderWrapper::GetConstantBuffers( int startSlot, int count )
	{
		array<Buffer^>^ buffers = gcnew array<Buffer^>( count );
		stack_array<ID3D11Buffer*> results = stackalloc( ID3D11Buffer*, count );

		deviceContext->CSGetConstantBuffers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			buffers[i] = Buffer::FromPointer( results[i] );

		return buffers;
	}

	array<SamplerState^>^ ComputeShaderWrapper::GetSamplers( int startSlot, int count )
	{
		array<SamplerState^>^ samplers = gcnew array<SamplerState^>( count );
		stack_array<ID3D11SamplerState*> results = stackalloc( ID3D11SamplerState*, count );

		deviceContext->CSGetSamplers( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			samplers[i] = SamplerState::FromPointer( results[i] );

		return samplers;
	}

	array<ShaderResourceView^>^ ComputeShaderWrapper::GetShaderResources( int startSlot, int count )
	{
		array<ShaderResourceView^>^ resources = gcnew array<ShaderResourceView^>( count );
		stack_array<ID3D11ShaderResourceView*> results = stackalloc( ID3D11ShaderResourceView*, count );

		deviceContext->CSGetShaderResources( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			resources[i] = ShaderResourceView::FromPointer( results[i] );

		return resources;
	}

	array<UnorderedAccessView^>^ ComputeShaderWrapper::GetUnorderedAccessViews( int startSlot, int count )
	{
		array<UnorderedAccessView^>^ resources = gcnew array<UnorderedAccessView^>( count );
		stack_array<ID3D11UnorderedAccessView*> results = stackalloc( ID3D11UnorderedAccessView*, count );

		deviceContext->CSGetUnorderedAccessViews( startSlot, count, &results[0] );

		for( int i = 0; i < count; i++ )
			resources[i] = UnorderedAccessView::FromPointer( results[i] );

		return resources;
	}

	void ComputeShaderWrapper::SetConstantBuffer( Buffer^ constantBuffer, int slot )
	{
		ID3D11Buffer *buffer = constantBuffer == nullptr ? NULL : constantBuffer->InternalPointer;
		deviceContext->CSSetConstantBuffers( slot, 1, &buffer );
	}

	void ComputeShaderWrapper::SetConstantBuffers( array<Buffer^>^ constantBuffers, int startSlot, int count )
	{
		if( count > constantBuffers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11Buffer*> input = stackalloc( ID3D11Buffer*, count );
		for( int i = 0; i < count; i++ )
			input[i] = constantBuffers[i] == nullptr ? NULL : constantBuffers[i]->InternalPointer;

		deviceContext->CSSetConstantBuffers( startSlot, count, &input[0] );
	}

	void ComputeShaderWrapper::SetSampler( SamplerState^ sampler, int slot )
	{
		ID3D11SamplerState *pointer = sampler == nullptr ? NULL : sampler->InternalPointer;
		deviceContext->CSSetSamplers( slot, 1, &pointer );
	}

	void ComputeShaderWrapper::SetSamplers( array<SamplerState^>^ samplers, int startSlot, int count )
	{
		if( count > samplers->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11SamplerState*> input = stackalloc( ID3D11SamplerState*, count );
		for( int i = 0; i < count; i++ )
			input[i] = samplers[i] == nullptr ? NULL : samplers[i]->InternalPointer;

		deviceContext->CSSetSamplers( startSlot, count, &input[0] );
	}

	void ComputeShaderWrapper::SetShaderResource( ShaderResourceView^ resourceView, int slot )
	{
		ID3D11ShaderResourceView *resource = resourceView == nullptr ? NULL : resourceView->InternalPointer;
		deviceContext->CSSetShaderResources( slot, 1, &resource );
	}

	void ComputeShaderWrapper::SetShaderResources( array<ShaderResourceView^>^ resourceViews, int startSlot, int count )
	{
		if( count > resourceViews->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11ShaderResourceView*> input = stackalloc( ID3D11ShaderResourceView*, count );
		for( int i = 0; i < count; i++ )
			input[i] = resourceViews[i] == nullptr ? NULL : resourceViews[i]->InternalPointer;

		deviceContext->CSSetShaderResources( startSlot, count, &input[0] );
	}

	void ComputeShaderWrapper::SetUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, int slot )
	{
		SetUnorderedAccessView( unorderedAccessView, slot, -1 );
	}

	void ComputeShaderWrapper::SetUnorderedAccessViews( array<UnorderedAccessView^>^ unorderedAccessViews, int startSlot, int count )
	{
		array<int>^ lengths = gcnew array<int>( count );
		for( int i = 0; i < count; i++ )
			lengths[i] = -1;

		SetUnorderedAccessViews( unorderedAccessViews, startSlot, count, lengths );
	}

	void ComputeShaderWrapper::SetUnorderedAccessView( UnorderedAccessView^ unorderedAccessView, int slot, int initialLength  )
	{
		UINT nativeLength = initialLength;
		ID3D11UnorderedAccessView *resource = unorderedAccessView == nullptr ? NULL : unorderedAccessView->InternalPointer;
		deviceContext->CSSetUnorderedAccessViews( slot, 1, &resource, &nativeLength );
	}

	void ComputeShaderWrapper::SetUnorderedAccessViews( array<UnorderedAccessView^>^ unorderedAccessViews, int startSlot, int count, array<int>^ initialLengths )
	{
		if( count > unorderedAccessViews->Length )
			throw gcnew ArgumentOutOfRangeException( "count" );

		stack_array<ID3D11UnorderedAccessView*> input = stackalloc( ID3D11UnorderedAccessView*, count );
		for( int i = 0; i < count; i++ )
			input[i] = unorderedAccessViews[i] == nullptr ? NULL : unorderedAccessViews[i]->InternalPointer;

		pin_ptr<int> pinnedLengths = &initialLengths[0];
		deviceContext->CSSetUnorderedAccessViews( startSlot, count, &input[0], reinterpret_cast<UINT*>( pinnedLengths ) );
	}
}
}
