using GTA.Math;
using GTA.Native;
using System;

namespace GTA
{
	public sealed class Rope : IEquatable<Rope>, IHandleable
	{
		public Rope(int handle)
		{
			Handle = handle;
		}

		public int Handle
		{
			get; private set;
		}

		public int VertexCount => Function.Call<int>(Hash.GET_ROPE_VERTEX_COUNT, Handle);

		public float Length
		{
			get => Function.Call<float>(Hash._GET_ROPE_LENGTH, Handle);
			set => Function.Call(Hash.ROPE_FORCE_LENGTH, Handle, value);
		}

		public void ResetLength(bool reset)
		{
			Function.Call(Hash.ROPE_RESET_LENGTH, Handle, reset);
		}

		public void ActivatePhysics()
		{
			Function.Call(Hash.ACTIVATE_PHYSICS, Handle);
		}

		public void AttachEntity(Entity entity)
		{
			AttachEntity(entity, Vector3.Zero);
		}
		public void AttachEntity(Entity entity, Vector3 position)
		{
			Function.Call(Hash.ATTACH_ROPE_TO_ENTITY, Handle, entity.Handle, position.X, position.Y, position.Z, 0);
		}
		public void DetachEntity(Entity entity)
		{
			Function.Call(Hash.DETACH_ROPE_FROM_ENTITY, Handle, entity.Handle);
		}

		public void AttachEntities(Entity entityOne, Entity entityTwo, float length)
		{
			AttachEntities(entityOne, Vector3.Zero, entityTwo, Vector3.Zero, length);
		}
		public void AttachEntities(Entity entityOne, Vector3 positionOne, Entity entityTwo, Vector3 positionTwo, float length)
		{
			Function.Call(Hash.ATTACH_ENTITIES_TO_ROPE, Handle, entityOne.Handle, entityTwo.Handle, positionOne.X, positionOne.Y, positionOne.Z, positionTwo.X, positionTwo.Y, positionTwo.Z, length, 0, 0, 0, 0);
		}

		public void PinVertex(int vertex, Vector3 position)
		{
			Function.Call(Hash.PIN_ROPE_VERTEX, Handle, vertex, position.X, position.Y, position.Z);
		}
		public void UnpinVertex(int vertex)
		{
			Function.Call(Hash.UNPIN_ROPE_VERTEX, Handle, vertex);
		}
		public Vector3 GetVertexCoord(int vertex)
		{
			return Function.Call<Vector3>(Hash.GET_ROPE_VERTEX_COORD, Handle, vertex);
		}

		public void Delete()
		{
			int handle = Handle;
			unsafe
			{
				Function.Call(Hash.DELETE_ROPE, &handle);
			}
			Handle = handle;
		}

		public bool Exists()
		{
			int handle = Handle;
			unsafe
			{
				return Function.Call<bool>(Hash.DOES_ROPE_EXIST, &handle);
			}
		}
		public static bool Exists(Rope rope)
		{
			return !ReferenceEquals(rope, null) && rope.Exists();
		}

		public bool Equals(Rope rope)
		{
			return !ReferenceEquals(rope, null) && Handle == rope.Handle;
		}
		public override bool Equals(object obj)
		{
			return !ReferenceEquals(obj, null) && obj.GetType() == GetType() && Equals((Rope)obj);
		}

		public override int GetHashCode()
		{
			return Handle;
		}

		public static bool operator ==(Rope left, Rope right)
		{
			return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);
		}
		public static bool operator !=(Rope left, Rope right)
		{
			return !(left == right);
		}
	}
}