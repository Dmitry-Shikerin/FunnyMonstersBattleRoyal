using System;
using UnityEngine;
using Leopotam.EcsProto;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Components;
using Sources.EcsBoundedContexts.Weapons.Domain;
using Sources.EcsBoundedContexts.Weapons.Presentation;
using Sources.EcsBoundedContexts.Volumes.Domain.Components;
using Sources.EcsBoundedContexts.Volumes.Presentation;
using Sources.EcsBoundedContexts.Volumes.Domain.Enums;
using Sources.EcsBoundedContexts.Tutorials.Domain.Components;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.PlayerWallets.Presentation;
using Sources.EcsBoundedContexts.Particles.Domain;
using Sources.EcsBoundedContexts.Movements.TargetPoint.Components;
using Sources.EcsBoundedContexts.Movements.Rotation.Components;
using Sources.EcsBoundedContexts.Movements.Rotation.Data;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using UnityEngine.AI;
using Sources.EcsBoundedContexts.LookAt.Domain;
using Sources.EcsBoundedContexts.Lights.Domain;
using UnityEngine.Rendering;
using Sources.EcsBoundedContexts.Lights.Domain.Enums;
using Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Components;
using Sources.EcsBoundedContexts.Input.Domain;
using Sources.EcsBoundedContexts.GraphOwners.Domain;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.StateMachines;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.ExplosionBodies.Domain;
using Sources.EcsBoundedContexts.Damage.Domain;
using UnityEngine.UI;
using TMPro;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.EcsBoundedContexts.DailyRewards.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using DG.Tweening;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Unity.Cinemachine;
using Sources.EcsBoundedContexts.Animators;
using Sources.EcsBoundedContexts.AnimatorLod.Domain.Components;
using Sources.EcsBoundedContexts.Animancers.Domain.Components;
using Animancer;

namespace Sources.EcsBoundedContexts.Core
{
	public static class GameAspectExt
	{
		private static GameAspect s_GameAspect;

		public static void Construct(GameAspect aspect) =>
			s_GameAspect = aspect ?? throw new ArgumentNullException(nameof(aspect));

		//EventBuffer
		public static bool HasEventBuffer(this ProtoEntity entity) =>
			s_GameAspect.EventBuffer.Has(entity);

		public static ref EventBufferTag AddEventBuffer(this ProtoEntity entity)
		{
			ref EventBufferTag eventBufferTag = ref s_GameAspect.EventBuffer.Add(entity);
			return ref eventBufferTag;
		}

		public static void DelEventBuffer(this ProtoEntity entity)
			=> s_GameAspect.EventBuffer.Del(entity);

		//ReturnToPoolAction
		public static bool HasReturnToPoolAction(this ProtoEntity entity) =>
			s_GameAspect.ReturnToPoolAction.Has(entity);

		public static ref ReturnToPoolActionComponent GetReturnToPoolAction(this ProtoEntity entity) =>
			ref s_GameAspect.ReturnToPoolAction.Get(entity);

		public static void ReplaceReturnToPoolAction(this ProtoEntity entity, Action returnToPool)
		{
			ref ReturnToPoolActionComponent returnToPoolActionComponent = ref s_GameAspect.ReturnToPoolAction.Get(entity);
			returnToPoolActionComponent.ReturnToPool = returnToPool;
		}

		public static ref ReturnToPoolActionComponent AddReturnToPoolAction(this ProtoEntity entity, Action returnToPool)
		{
			ref ReturnToPoolActionComponent returnToPoolActionComponent = ref s_GameAspect.ReturnToPoolAction.Add(entity);
			returnToPoolActionComponent.ReturnToPool = returnToPool;
			return ref returnToPoolActionComponent;
		}

		public static void DelReturnToPoolAction(this ProtoEntity entity)
			=> s_GameAspect.ReturnToPoolAction.Del(entity);

		//GunOwnerModule
		public static bool HasGunOwnerModule(this ProtoEntity entity) =>
			s_GameAspect.GunOwnerModule.Has(entity);

		public static ref GunOwnerModuleComponent GetGunOwnerModule(this ProtoEntity entity) =>
			ref s_GameAspect.GunOwnerModule.Get(entity);

		public static void ReplaceGunOwnerModule(this ProtoEntity entity, GunOwnerModule value)
		{
			ref GunOwnerModuleComponent gunOwnerModuleComponent = ref s_GameAspect.GunOwnerModule.Get(entity);
			gunOwnerModuleComponent.Value = value;
		}

		public static ref GunOwnerModuleComponent AddGunOwnerModule(this ProtoEntity entity, GunOwnerModule value)
		{
			ref GunOwnerModuleComponent gunOwnerModuleComponent = ref s_GameAspect.GunOwnerModule.Add(entity);
			gunOwnerModuleComponent.Value = value;
			return ref gunOwnerModuleComponent;
		}

		public static void DelGunOwnerModule(this ProtoEntity entity)
			=> s_GameAspect.GunOwnerModule.Del(entity);

		//ChangeVolumeEvent
		public static bool HasChangeVolumeEvent(this ProtoEntity entity) =>
			s_GameAspect.ChangeVolumeEvent.Has(entity);

		public static ref ChangeVolumeEvent GetChangeVolumeEvent(this ProtoEntity entity) =>
			ref s_GameAspect.ChangeVolumeEvent.Get(entity);

		public static void ReplaceChangeVolumeEvent(this ProtoEntity entity, float value)
		{
			ref ChangeVolumeEvent changeVolumeEvent = ref s_GameAspect.ChangeVolumeEvent.Get(entity);
			changeVolumeEvent.Value = value;
		}

		public static ref ChangeVolumeEvent AddChangeVolumeEvent(this ProtoEntity entity, float value)
		{
			ref ChangeVolumeEvent changeVolumeEvent = ref s_GameAspect.ChangeVolumeEvent.Add(entity);
			changeVolumeEvent.Value = value;
			return ref changeVolumeEvent;
		}

		public static void DelChangeVolumeEvent(this ProtoEntity entity)
			=> s_GameAspect.ChangeVolumeEvent.Del(entity);

		//GameVolume
		public static bool HasGameVolume(this ProtoEntity entity) =>
			s_GameAspect.GameVolume.Has(entity);

		public static ref GameVolumeComponent GetGameVolume(this ProtoEntity entity) =>
			ref s_GameAspect.GameVolume.Get(entity);

		public static void ReplaceGameVolume(this ProtoEntity entity, float value)
		{
			ref GameVolumeComponent gameVolumeComponent = ref s_GameAspect.GameVolume.Get(entity);
			gameVolumeComponent.Value = value;
		}

		public static ref GameVolumeComponent AddGameVolume(this ProtoEntity entity, float value)
		{
			ref GameVolumeComponent gameVolumeComponent = ref s_GameAspect.GameVolume.Add(entity);
			gameVolumeComponent.Value = value;
			return ref gameVolumeComponent;
		}

		public static void DelGameVolume(this ProtoEntity entity)
			=> s_GameAspect.GameVolume.Del(entity);

		//MutedVolume
		public static bool HasMutedVolume(this ProtoEntity entity) =>
			s_GameAspect.MutedVolume.Has(entity);

		public static ref MutedVolumeComponent AddMutedVolume(this ProtoEntity entity)
		{
			ref MutedVolumeComponent mutedVolumeComponent = ref s_GameAspect.MutedVolume.Add(entity);
			return ref mutedVolumeComponent;
		}

		public static void DelMutedVolume(this ProtoEntity entity)
			=> s_GameAspect.MutedVolume.Del(entity);

		//MuteVolumeEvent
		public static bool HasMuteVolumeEvent(this ProtoEntity entity) =>
			s_GameAspect.MuteVolumeEvent.Has(entity);

		public static ref MuteVolumeEvent AddMuteVolumeEvent(this ProtoEntity entity)
		{
			ref MuteVolumeEvent muteVolumeEvent = ref s_GameAspect.MuteVolumeEvent.Add(entity);
			return ref muteVolumeEvent;
		}

		public static void DelMuteVolumeEvent(this ProtoEntity entity)
			=> s_GameAspect.MuteVolumeEvent.Del(entity);

		//UnmuteVolumeEvent
		public static bool HasUnmuteVolumeEvent(this ProtoEntity entity) =>
			s_GameAspect.UnmuteVolumeEvent.Has(entity);

		public static ref UnmuteVolumeEvent AddUnmuteVolumeEvent(this ProtoEntity entity)
		{
			ref UnmuteVolumeEvent unmuteVolumeEvent = ref s_GameAspect.UnmuteVolumeEvent.Add(entity);
			return ref unmuteVolumeEvent;
		}

		public static void DelUnmuteVolumeEvent(this ProtoEntity entity)
			=> s_GameAspect.UnmuteVolumeEvent.Del(entity);

		//VolumeModule
		public static bool HasVolumeModule(this ProtoEntity entity) =>
			s_GameAspect.VolumeModule.Has(entity);

		public static ref VolumeModuleComponent GetVolumeModule(this ProtoEntity entity) =>
			ref s_GameAspect.VolumeModule.Get(entity);

		public static void ReplaceVolumeModule(this ProtoEntity entity, VolumeModule value)
		{
			ref VolumeModuleComponent volumeModuleComponent = ref s_GameAspect.VolumeModule.Get(entity);
			volumeModuleComponent.Value = value;
		}

		public static ref VolumeModuleComponent AddVolumeModule(this ProtoEntity entity, VolumeModule value)
		{
			ref VolumeModuleComponent volumeModuleComponent = ref s_GameAspect.VolumeModule.Add(entity);
			volumeModuleComponent.Value = value;
			return ref volumeModuleComponent;
		}

		public static void DelVolumeModule(this ProtoEntity entity)
			=> s_GameAspect.VolumeModule.Del(entity);

		//VolumeType
		public static bool HasVolumeType(this ProtoEntity entity) =>
			s_GameAspect.VolumeType.Has(entity);

		public static ref VolumeTypeComponent GetVolumeType(this ProtoEntity entity) =>
			ref s_GameAspect.VolumeType.Get(entity);

		public static void ReplaceVolumeType(this ProtoEntity entity, VolumeType value)
		{
			ref VolumeTypeComponent volumeTypeComponent = ref s_GameAspect.VolumeType.Get(entity);
			volumeTypeComponent.Value = value;
		}

		public static ref VolumeTypeComponent AddVolumeType(this ProtoEntity entity, VolumeType value)
		{
			ref VolumeTypeComponent volumeTypeComponent = ref s_GameAspect.VolumeType.Add(entity);
			volumeTypeComponent.Value = value;
			return ref volumeTypeComponent;
		}

		public static void DelVolumeType(this ProtoEntity entity)
			=> s_GameAspect.VolumeType.Del(entity);

		//Tutorial
		public static bool HasTutorial(this ProtoEntity entity) =>
			s_GameAspect.Tutorial.Has(entity);

		public static ref TutorialTag AddTutorial(this ProtoEntity entity)
		{
			ref TutorialTag tutorialTag = ref s_GameAspect.Tutorial.Add(entity);
			return ref tutorialTag;
		}

		public static void DelTutorial(this ProtoEntity entity)
			=> s_GameAspect.Tutorial.Del(entity);

		//Timer
		public static bool HasTimer(this ProtoEntity entity) =>
			s_GameAspect.Timer.Has(entity);

		public static ref TimerComponent GetTimer(this ProtoEntity entity) =>
			ref s_GameAspect.Timer.Get(entity);

		public static void ReplaceTimer(this ProtoEntity entity, float value)
		{
			ref TimerComponent timerComponent = ref s_GameAspect.Timer.Get(entity);
			timerComponent.Value = value;
		}

		public static ref TimerComponent AddTimer(this ProtoEntity entity, float value)
		{
			ref TimerComponent timerComponent = ref s_GameAspect.Timer.Add(entity);
			timerComponent.Value = value;
			return ref timerComponent;
		}

		public static void DelTimer(this ProtoEntity entity)
			=> s_GameAspect.Timer.Del(entity);

		//ClearableData
		public static bool HasClearableData(this ProtoEntity entity) =>
			s_GameAspect.ClearableData.Has(entity);

		public static ref ClearableDataComponent AddClearableData(this ProtoEntity entity)
		{
			ref ClearableDataComponent clearableDataComponent = ref s_GameAspect.ClearableData.Add(entity);
			return ref clearableDataComponent;
		}

		public static void DelClearableData(this ProtoEntity entity)
			=> s_GameAspect.ClearableData.Del(entity);

		//ClearDataEvent
		public static bool HasClearDataEvent(this ProtoEntity entity) =>
			s_GameAspect.ClearDataEvent.Has(entity);

		public static ref ClearDataEvent AddClearDataEvent(this ProtoEntity entity)
		{
			ref ClearDataEvent clearDataEvent = ref s_GameAspect.ClearDataEvent.Add(entity);
			return ref clearDataEvent;
		}

		public static void DelClearDataEvent(this ProtoEntity entity)
			=> s_GameAspect.ClearDataEvent.Del(entity);

		//SavableData
		public static bool HasSavableData(this ProtoEntity entity) =>
			s_GameAspect.SavableData.Has(entity);

		public static ref SavableDataComponent AddSavableData(this ProtoEntity entity)
		{
			ref SavableDataComponent savableDataComponent = ref s_GameAspect.SavableData.Add(entity);
			return ref savableDataComponent;
		}

		public static void DelSavableData(this ProtoEntity entity)
			=> s_GameAspect.SavableData.Del(entity);

		//SaveDataEvent
		public static bool HasSaveDataEvent(this ProtoEntity entity) =>
			s_GameAspect.SaveDataEvent.Has(entity);

		public static ref SaveDataEvent AddSaveDataEvent(this ProtoEntity entity)
		{
			ref SaveDataEvent saveDataEvent = ref s_GameAspect.SaveDataEvent.Add(entity);
			return ref saveDataEvent;
		}

		public static void DelSaveDataEvent(this ProtoEntity entity)
			=> s_GameAspect.SaveDataEvent.Del(entity);

		//CoinsChangedEvent
		public static bool HasCoinsChangedEvent(this ProtoEntity entity) =>
			s_GameAspect.CoinsChangedEvent.Has(entity);

		public static ref CoinsChangedEvent AddCoinsChangedEvent(this ProtoEntity entity)
		{
			ref CoinsChangedEvent coinsChangedEvent = ref s_GameAspect.CoinsChangedEvent.Add(entity);
			return ref coinsChangedEvent;
		}

		public static void DelCoinsChangedEvent(this ProtoEntity entity)
			=> s_GameAspect.CoinsChangedEvent.Del(entity);

		//DecreaseCoinsEvent
		public static bool HasDecreaseCoinsEvent(this ProtoEntity entity) =>
			s_GameAspect.DecreaseCoinsEvent.Has(entity);

		public static ref DecreaseCoinsEvent GetDecreaseCoinsEvent(this ProtoEntity entity) =>
			ref s_GameAspect.DecreaseCoinsEvent.Get(entity);

		public static void ReplaceDecreaseCoinsEvent(this ProtoEntity entity, int value)
		{
			ref DecreaseCoinsEvent decreaseCoinsEvent = ref s_GameAspect.DecreaseCoinsEvent.Get(entity);
			decreaseCoinsEvent.Value = value;
		}

		public static ref DecreaseCoinsEvent AddDecreaseCoinsEvent(this ProtoEntity entity, int value)
		{
			ref DecreaseCoinsEvent decreaseCoinsEvent = ref s_GameAspect.DecreaseCoinsEvent.Add(entity);
			decreaseCoinsEvent.Value = value;
			return ref decreaseCoinsEvent;
		}

		public static void DelDecreaseCoinsEvent(this ProtoEntity entity)
			=> s_GameAspect.DecreaseCoinsEvent.Del(entity);

		//IncreaseCoinsEvent
		public static bool HasIncreaseCoinsEvent(this ProtoEntity entity) =>
			s_GameAspect.IncreaseCoinsEvent.Has(entity);

		public static ref IncreaseCoinsEvent GetIncreaseCoinsEvent(this ProtoEntity entity) =>
			ref s_GameAspect.IncreaseCoinsEvent.Get(entity);

		public static void ReplaceIncreaseCoinsEvent(this ProtoEntity entity, int value)
		{
			ref IncreaseCoinsEvent increaseCoinsEvent = ref s_GameAspect.IncreaseCoinsEvent.Get(entity);
			increaseCoinsEvent.Value = value;
		}

		public static ref IncreaseCoinsEvent AddIncreaseCoinsEvent(this ProtoEntity entity, int value)
		{
			ref IncreaseCoinsEvent increaseCoinsEvent = ref s_GameAspect.IncreaseCoinsEvent.Add(entity);
			increaseCoinsEvent.Value = value;
			return ref increaseCoinsEvent;
		}

		public static void DelIncreaseCoinsEvent(this ProtoEntity entity)
			=> s_GameAspect.IncreaseCoinsEvent.Del(entity);

		//PlayerWallet
		public static bool HasPlayerWallet(this ProtoEntity entity) =>
			s_GameAspect.PlayerWallet.Has(entity);

		public static ref PlayerWalletComponent GetPlayerWallet(this ProtoEntity entity) =>
			ref s_GameAspect.PlayerWallet.Get(entity);

		public static void ReplacePlayerWallet(this ProtoEntity entity, int value)
		{
			ref PlayerWalletComponent playerWalletComponent = ref s_GameAspect.PlayerWallet.Get(entity);
			playerWalletComponent.Value = value;
		}

		public static ref PlayerWalletComponent AddPlayerWallet(this ProtoEntity entity, int value)
		{
			ref PlayerWalletComponent playerWalletComponent = ref s_GameAspect.PlayerWallet.Add(entity);
			playerWalletComponent.Value = value;
			return ref playerWalletComponent;
		}

		public static void DelPlayerWallet(this ProtoEntity entity)
			=> s_GameAspect.PlayerWallet.Del(entity);

		//PlayerWalletModule
		public static bool HasPlayerWalletModule(this ProtoEntity entity) =>
			s_GameAspect.PlayerWalletModule.Has(entity);

		public static ref PlayerWalletModuleComponent GetPlayerWalletModule(this ProtoEntity entity) =>
			ref s_GameAspect.PlayerWalletModule.Get(entity);

		public static void ReplacePlayerWalletModule(this ProtoEntity entity, PlayerWalletModule value)
		{
			ref PlayerWalletModuleComponent playerWalletModuleComponent = ref s_GameAspect.PlayerWalletModule.Get(entity);
			playerWalletModuleComponent.Value = value;
		}

		public static ref PlayerWalletModuleComponent AddPlayerWalletModule(this ProtoEntity entity, PlayerWalletModule value)
		{
			ref PlayerWalletModuleComponent playerWalletModuleComponent = ref s_GameAspect.PlayerWalletModule.Add(entity);
			playerWalletModuleComponent.Value = value;
			return ref playerWalletModuleComponent;
		}

		public static void DelPlayerWalletModule(this ProtoEntity entity)
			=> s_GameAspect.PlayerWalletModule.Del(entity);

		//HealParticle
		public static bool HasHealParticle(this ProtoEntity entity) =>
			s_GameAspect.HealParticle.Has(entity);

		public static ref HealParticleComponent GetHealParticle(this ProtoEntity entity) =>
			ref s_GameAspect.HealParticle.Get(entity);

		public static void ReplaceHealParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref HealParticleComponent healParticleComponent = ref s_GameAspect.HealParticle.Get(entity);
			healParticleComponent.Value = value;
		}

		public static ref HealParticleComponent AddHealParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref HealParticleComponent healParticleComponent = ref s_GameAspect.HealParticle.Add(entity);
			healParticleComponent.Value = value;
			return ref healParticleComponent;
		}

		public static void DelHealParticle(this ProtoEntity entity)
			=> s_GameAspect.HealParticle.Del(entity);

		//ShootParticle
		public static bool HasShootParticle(this ProtoEntity entity) =>
			s_GameAspect.ShootParticle.Has(entity);

		public static ref ShootParticleComponent GetShootParticle(this ProtoEntity entity) =>
			ref s_GameAspect.ShootParticle.Get(entity);

		public static void ReplaceShootParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref ShootParticleComponent shootParticleComponent = ref s_GameAspect.ShootParticle.Get(entity);
			shootParticleComponent.Value = value;
		}

		public static ref ShootParticleComponent AddShootParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref ShootParticleComponent shootParticleComponent = ref s_GameAspect.ShootParticle.Add(entity);
			shootParticleComponent.Value = value;
			return ref shootParticleComponent;
		}

		public static void DelShootParticle(this ProtoEntity entity)
			=> s_GameAspect.ShootParticle.Del(entity);

		//PointPath
		public static bool HasPointPath(this ProtoEntity entity) =>
			s_GameAspect.PointPath.Has(entity);

		public static ref PointPathComponent GetPointPath(this ProtoEntity entity) =>
			ref s_GameAspect.PointPath.Get(entity);

		public static void ReplacePointPath(this ProtoEntity entity, Vector3[] points)
		{
			ref PointPathComponent pointPathComponent = ref s_GameAspect.PointPath.Get(entity);
			pointPathComponent.Points = points;
		}

		public static ref PointPathComponent AddPointPath(this ProtoEntity entity, Vector3[] points)
		{
			ref PointPathComponent pointPathComponent = ref s_GameAspect.PointPath.Add(entity);
			pointPathComponent.Points = points;
			return ref pointPathComponent;
		}

		public static void DelPointPath(this ProtoEntity entity)
			=> s_GameAspect.PointPath.Del(entity);

		//TargetPoint
		public static bool HasTargetPoint(this ProtoEntity entity) =>
			s_GameAspect.TargetPoint.Has(entity);

		public static ref TargetPointComponent GetTargetPoint(this ProtoEntity entity) =>
			ref s_GameAspect.TargetPoint.Get(entity);

		public static void ReplaceTargetPoint(this ProtoEntity entity, Vector3 value)
		{
			ref TargetPointComponent targetPointComponent = ref s_GameAspect.TargetPoint.Get(entity);
			targetPointComponent.Value = value;
		}

		public static ref TargetPointComponent AddTargetPoint(this ProtoEntity entity, Vector3 value)
		{
			ref TargetPointComponent targetPointComponent = ref s_GameAspect.TargetPoint.Add(entity);
			targetPointComponent.Value = value;
			return ref targetPointComponent;
		}

		public static void DelTargetPoint(this ProtoEntity entity)
			=> s_GameAspect.TargetPoint.Del(entity);

		//TargetPointIndex
		public static bool HasTargetPointIndex(this ProtoEntity entity) =>
			s_GameAspect.TargetPointIndex.Has(entity);

		public static ref TargetPointIndexComponent GetTargetPointIndex(this ProtoEntity entity) =>
			ref s_GameAspect.TargetPointIndex.Get(entity);

		public static void ReplaceTargetPointIndex(this ProtoEntity entity, int value)
		{
			ref TargetPointIndexComponent targetPointIndexComponent = ref s_GameAspect.TargetPointIndex.Get(entity);
			targetPointIndexComponent.Value = value;
		}

		public static ref TargetPointIndexComponent AddTargetPointIndex(this ProtoEntity entity, int value)
		{
			ref TargetPointIndexComponent targetPointIndexComponent = ref s_GameAspect.TargetPointIndex.Add(entity);
			targetPointIndexComponent.Value = value;
			return ref targetPointIndexComponent;
		}

		public static void DelTargetPointIndex(this ProtoEntity entity)
			=> s_GameAspect.TargetPointIndex.Del(entity);

		//ChangeRotationSpeedDelta
		public static bool HasChangeRotationSpeedDelta(this ProtoEntity entity) =>
			s_GameAspect.ChangeRotationSpeedDelta.Has(entity);

		public static ref ChangeRotationSpeedDeltaComponent GetChangeRotationSpeedDelta(this ProtoEntity entity) =>
			ref s_GameAspect.ChangeRotationSpeedDelta.Get(entity);

		public static void ReplaceChangeRotationSpeedDelta(this ProtoEntity entity, float value)
		{
			ref ChangeRotationSpeedDeltaComponent changeRotationSpeedDeltaComponent = ref s_GameAspect.ChangeRotationSpeedDelta.Get(entity);
			changeRotationSpeedDeltaComponent.Value = value;
		}

		public static ref ChangeRotationSpeedDeltaComponent AddChangeRotationSpeedDelta(this ProtoEntity entity, float value)
		{
			ref ChangeRotationSpeedDeltaComponent changeRotationSpeedDeltaComponent = ref s_GameAspect.ChangeRotationSpeedDelta.Add(entity);
			changeRotationSpeedDeltaComponent.Value = value;
			return ref changeRotationSpeedDeltaComponent;
		}

		public static void DelChangeRotationSpeedDelta(this ProtoEntity entity)
			=> s_GameAspect.ChangeRotationSpeedDelta.Del(entity);

		//RotationSpeed
		public static bool HasRotationSpeed(this ProtoEntity entity) =>
			s_GameAspect.RotationSpeed.Has(entity);

		public static ref RotationSpeedComponent GetRotationSpeed(this ProtoEntity entity) =>
			ref s_GameAspect.RotationSpeed.Get(entity);

		public static void ReplaceRotationSpeed(this ProtoEntity entity, float value)
		{
			ref RotationSpeedComponent rotationSpeedComponent = ref s_GameAspect.RotationSpeed.Get(entity);
			rotationSpeedComponent.Value = value;
		}

		public static ref RotationSpeedComponent AddRotationSpeed(this ProtoEntity entity, float value)
		{
			ref RotationSpeedComponent rotationSpeedComponent = ref s_GameAspect.RotationSpeed.Add(entity);
			rotationSpeedComponent.Value = value;
			return ref rotationSpeedComponent;
		}

		public static void DelRotationSpeed(this ProtoEntity entity)
			=> s_GameAspect.RotationSpeed.Del(entity);

		//TargetRotation
		public static bool HasTargetRotation(this ProtoEntity entity) =>
			s_GameAspect.TargetRotation.Has(entity);

		public static ref TargetRotationComponent GetTargetRotation(this ProtoEntity entity) =>
			ref s_GameAspect.TargetRotation.Get(entity);

		public static void ReplaceTargetRotation(this ProtoEntity entity, Quaternion value)
		{
			ref TargetRotationComponent targetRotationComponent = ref s_GameAspect.TargetRotation.Get(entity);
			targetRotationComponent.Value = value;
		}

		public static ref TargetRotationComponent AddTargetRotation(this ProtoEntity entity, Quaternion value)
		{
			ref TargetRotationComponent targetRotationComponent = ref s_GameAspect.TargetRotation.Add(entity);
			targetRotationComponent.Value = value;
			return ref targetRotationComponent;
		}

		public static void DelTargetRotation(this ProtoEntity entity)
			=> s_GameAspect.TargetRotation.Del(entity);

		//TargetRotationSpeed
		public static bool HasTargetRotationSpeed(this ProtoEntity entity) =>
			s_GameAspect.TargetRotationSpeed.Has(entity);

		public static ref TargetRotationSpeedComponent GetTargetRotationSpeed(this ProtoEntity entity) =>
			ref s_GameAspect.TargetRotationSpeed.Get(entity);

		public static void ReplaceTargetRotationSpeed(this ProtoEntity entity, float value)
		{
			ref TargetRotationSpeedComponent targetRotationSpeedComponent = ref s_GameAspect.TargetRotationSpeed.Get(entity);
			targetRotationSpeedComponent.Value = value;
		}

		public static ref TargetRotationSpeedComponent AddTargetRotationSpeed(this ProtoEntity entity, float value)
		{
			ref TargetRotationSpeedComponent targetRotationSpeedComponent = ref s_GameAspect.TargetRotationSpeed.Add(entity);
			targetRotationSpeedComponent.Value = value;
			return ref targetRotationSpeedComponent;
		}

		public static void DelTargetRotationSpeed(this ProtoEntity entity)
			=> s_GameAspect.TargetRotationSpeed.Del(entity);

		//TurnSide
		public static bool HasTurnSide(this ProtoEntity entity) =>
			s_GameAspect.TurnSide.Has(entity);

		public static ref TurnSideComponent GetTurnSide(this ProtoEntity entity) =>
			ref s_GameAspect.TurnSide.Get(entity);

		public static void ReplaceTurnSide(this ProtoEntity entity, TurnSideType value)
		{
			ref TurnSideComponent turnSideComponent = ref s_GameAspect.TurnSide.Get(entity);
			turnSideComponent.Value = value;
		}

		public static ref TurnSideComponent AddTurnSide(this ProtoEntity entity, TurnSideType value)
		{
			ref TurnSideComponent turnSideComponent = ref s_GameAspect.TurnSide.Add(entity);
			turnSideComponent.Value = value;
			return ref turnSideComponent;
		}

		public static void DelTurnSide(this ProtoEntity entity)
			=> s_GameAspect.TurnSide.Del(entity);

		//ChangeSpeedDelta
		public static bool HasChangeSpeedDelta(this ProtoEntity entity) =>
			s_GameAspect.ChangeSpeedDelta.Has(entity);

		public static ref ChangeSpeedDeltaComponent GetChangeSpeedDelta(this ProtoEntity entity) =>
			ref s_GameAspect.ChangeSpeedDelta.Get(entity);

		public static void ReplaceChangeSpeedDelta(this ProtoEntity entity, float value)
		{
			ref ChangeSpeedDeltaComponent changeSpeedDeltaComponent = ref s_GameAspect.ChangeSpeedDelta.Get(entity);
			changeSpeedDeltaComponent.Value = value;
		}

		public static ref ChangeSpeedDeltaComponent AddChangeSpeedDelta(this ProtoEntity entity, float value)
		{
			ref ChangeSpeedDeltaComponent changeSpeedDeltaComponent = ref s_GameAspect.ChangeSpeedDelta.Add(entity);
			changeSpeedDeltaComponent.Value = value;
			return ref changeSpeedDeltaComponent;
		}

		public static void DelChangeSpeedDelta(this ProtoEntity entity)
			=> s_GameAspect.ChangeSpeedDelta.Del(entity);

		//CharacterController
		public static bool HasCharacterController(this ProtoEntity entity) =>
			s_GameAspect.CharacterController.Has(entity);

		public static ref CharacterControllerComponent GetCharacterController(this ProtoEntity entity) =>
			ref s_GameAspect.CharacterController.Get(entity);

		public static void ReplaceCharacterController(this ProtoEntity entity, CharacterController value)
		{
			ref CharacterControllerComponent characterControllerComponent = ref s_GameAspect.CharacterController.Get(entity);
			characterControllerComponent.Value = value;
		}

		public static ref CharacterControllerComponent AddCharacterController(this ProtoEntity entity, CharacterController value)
		{
			ref CharacterControllerComponent characterControllerComponent = ref s_GameAspect.CharacterController.Add(entity);
			characterControllerComponent.Value = value;
			return ref characterControllerComponent;
		}

		public static void DelCharacterController(this ProtoEntity entity)
			=> s_GameAspect.CharacterController.Del(entity);

		//CompleteMoveAlongPathEvent
		public static bool HasCompleteMoveAlongPathEvent(this ProtoEntity entity) =>
			s_GameAspect.CompleteMoveAlongPathEvent.Has(entity);

		public static ref CompleteMoveAlongPathEvent AddCompleteMoveAlongPathEvent(this ProtoEntity entity)
		{
			ref CompleteMoveAlongPathEvent completeMoveAlongPathEvent = ref s_GameAspect.CompleteMoveAlongPathEvent.Add(entity);
			return ref completeMoveAlongPathEvent;
		}

		public static void DelCompleteMoveAlongPathEvent(this ProtoEntity entity)
			=> s_GameAspect.CompleteMoveAlongPathEvent.Del(entity);

		//CompleteMoveAlongPathPointEvent
		public static bool HasCompleteMoveAlongPathPointEvent(this ProtoEntity entity) =>
			s_GameAspect.CompleteMoveAlongPathPointEvent.Has(entity);

		public static ref CompleteMoveAlongPathPointEvent GetCompleteMoveAlongPathPointEvent(this ProtoEntity entity) =>
			ref s_GameAspect.CompleteMoveAlongPathPointEvent.Get(entity);

		public static void ReplaceCompleteMoveAlongPathPointEvent(this ProtoEntity entity, int index)
		{
			ref CompleteMoveAlongPathPointEvent completeMoveAlongPathPointEvent = ref s_GameAspect.CompleteMoveAlongPathPointEvent.Get(entity);
			completeMoveAlongPathPointEvent.Index = index;
		}

		public static ref CompleteMoveAlongPathPointEvent AddCompleteMoveAlongPathPointEvent(this ProtoEntity entity, int index)
		{
			ref CompleteMoveAlongPathPointEvent completeMoveAlongPathPointEvent = ref s_GameAspect.CompleteMoveAlongPathPointEvent.Add(entity);
			completeMoveAlongPathPointEvent.Index = index;
			return ref completeMoveAlongPathPointEvent;
		}

		public static void DelCompleteMoveAlongPathPointEvent(this ProtoEntity entity)
			=> s_GameAspect.CompleteMoveAlongPathPointEvent.Del(entity);

		//MoveDelta
		public static bool HasMoveDelta(this ProtoEntity entity) =>
			s_GameAspect.MoveDelta.Has(entity);

		public static ref MoveDeltaComponent GetMoveDelta(this ProtoEntity entity) =>
			ref s_GameAspect.MoveDelta.Get(entity);

		public static void ReplaceMoveDelta(this ProtoEntity entity, float value)
		{
			ref MoveDeltaComponent moveDeltaComponent = ref s_GameAspect.MoveDelta.Get(entity);
			moveDeltaComponent.Value = value;
		}

		public static ref MoveDeltaComponent AddMoveDelta(this ProtoEntity entity, float value)
		{
			ref MoveDeltaComponent moveDeltaComponent = ref s_GameAspect.MoveDelta.Add(entity);
			moveDeltaComponent.Value = value;
			return ref moveDeltaComponent;
		}

		public static void DelMoveDelta(this ProtoEntity entity)
			=> s_GameAspect.MoveDelta.Del(entity);

		//MoveSpeed
		public static bool HasMoveSpeed(this ProtoEntity entity) =>
			s_GameAspect.MoveSpeed.Has(entity);

		public static ref MoveSpeedComponent GetMoveSpeed(this ProtoEntity entity) =>
			ref s_GameAspect.MoveSpeed.Get(entity);

		public static void ReplaceMoveSpeed(this ProtoEntity entity, float value)
		{
			ref MoveSpeedComponent moveSpeedComponent = ref s_GameAspect.MoveSpeed.Get(entity);
			moveSpeedComponent.Value = value;
		}

		public static ref MoveSpeedComponent AddMoveSpeed(this ProtoEntity entity, float value)
		{
			ref MoveSpeedComponent moveSpeedComponent = ref s_GameAspect.MoveSpeed.Add(entity);
			moveSpeedComponent.Value = value;
			return ref moveSpeedComponent;
		}

		public static void DelMoveSpeed(this ProtoEntity entity)
			=> s_GameAspect.MoveSpeed.Del(entity);

		//NavMesh
		public static bool HasNavMesh(this ProtoEntity entity) =>
			s_GameAspect.NavMesh.Has(entity);

		public static ref NavMeshComponent GetNavMesh(this ProtoEntity entity) =>
			ref s_GameAspect.NavMesh.Get(entity);

		public static void ReplaceNavMesh(this ProtoEntity entity, NavMeshAgent value)
		{
			ref NavMeshComponent navMeshComponent = ref s_GameAspect.NavMesh.Get(entity);
			navMeshComponent.Value = value;
		}

		public static ref NavMeshComponent AddNavMesh(this ProtoEntity entity, NavMeshAgent value)
		{
			ref NavMeshComponent navMeshComponent = ref s_GameAspect.NavMesh.Add(entity);
			navMeshComponent.Value = value;
			return ref navMeshComponent;
		}

		public static void DelNavMesh(this ProtoEntity entity)
			=> s_GameAspect.NavMesh.Del(entity);

		//TargetSpeed
		public static bool HasTargetSpeed(this ProtoEntity entity) =>
			s_GameAspect.TargetSpeed.Has(entity);

		public static ref TargetSpeedComponent GetTargetSpeed(this ProtoEntity entity) =>
			ref s_GameAspect.TargetSpeed.Get(entity);

		public static void ReplaceTargetSpeed(this ProtoEntity entity, float value)
		{
			ref TargetSpeedComponent targetSpeedComponent = ref s_GameAspect.TargetSpeed.Get(entity);
			targetSpeedComponent.Value = value;
		}

		public static ref TargetSpeedComponent AddTargetSpeed(this ProtoEntity entity, float value)
		{
			ref TargetSpeedComponent targetSpeedComponent = ref s_GameAspect.TargetSpeed.Add(entity);
			targetSpeedComponent.Value = value;
			return ref targetSpeedComponent;
		}

		public static void DelTargetSpeed(this ProtoEntity entity)
			=> s_GameAspect.TargetSpeed.Del(entity);

		//LookAt
		public static bool HasLookAt(this ProtoEntity entity) =>
			s_GameAspect.LookAt.Has(entity);

		public static ref LookAtComponent GetLookAt(this ProtoEntity entity) =>
			ref s_GameAspect.LookAt.Get(entity);

		public static void ReplaceLookAt(this ProtoEntity entity, Transform value)
		{
			ref LookAtComponent lookAtComponent = ref s_GameAspect.LookAt.Get(entity);
			lookAtComponent.Value = value;
		}

		public static ref LookAtComponent AddLookAt(this ProtoEntity entity, Transform value)
		{
			ref LookAtComponent lookAtComponent = ref s_GameAspect.LookAt.Add(entity);
			lookAtComponent.Value = value;
			return ref lookAtComponent;
		}

		public static void DelLookAt(this ProtoEntity entity)
			=> s_GameAspect.LookAt.Del(entity);

		//FireLight
		public static bool HasFireLight(this ProtoEntity entity) =>
			s_GameAspect.FireLight.Has(entity);

		public static ref FireLightComponent GetFireLight(this ProtoEntity entity) =>
			ref s_GameAspect.FireLight.Get(entity);

		public static void ReplaceFireLight(this ProtoEntity entity, float m_MinFlickeringTime, float m_MaxFlickeringTime, float m_MinIntensity, float m_MaxIntensity, float m_IntensitySmoothing, float m_MaxPositionOffset, float m_PositionSmoothing, Light m_Light, float m_TargetIntensity, Vector3 m_DefaultPos, Vector3 m_TargetPos)
		{
			ref FireLightComponent fireLightComponent = ref s_GameAspect.FireLight.Get(entity);
			fireLightComponent.m_MinFlickeringTime = m_MinFlickeringTime;
			fireLightComponent.m_MaxFlickeringTime = m_MaxFlickeringTime;
			fireLightComponent.m_MinIntensity = m_MinIntensity;
			fireLightComponent.m_MaxIntensity = m_MaxIntensity;
			fireLightComponent.m_IntensitySmoothing = m_IntensitySmoothing;
			fireLightComponent.m_MaxPositionOffset = m_MaxPositionOffset;
			fireLightComponent.m_PositionSmoothing = m_PositionSmoothing;
			fireLightComponent.m_Light = m_Light;
			fireLightComponent.m_TargetIntensity = m_TargetIntensity;
			fireLightComponent.m_DefaultPos = m_DefaultPos;
			fireLightComponent.m_TargetPos = m_TargetPos;
		}

		public static ref FireLightComponent AddFireLight(this ProtoEntity entity, float m_MinFlickeringTime, float m_MaxFlickeringTime, float m_MinIntensity, float m_MaxIntensity, float m_IntensitySmoothing, float m_MaxPositionOffset, float m_PositionSmoothing, Light m_Light, float m_TargetIntensity, Vector3 m_DefaultPos, Vector3 m_TargetPos)
		{
			ref FireLightComponent fireLightComponent = ref s_GameAspect.FireLight.Add(entity);
			fireLightComponent.m_MinFlickeringTime = m_MinFlickeringTime;
			fireLightComponent.m_MaxFlickeringTime = m_MaxFlickeringTime;
			fireLightComponent.m_MinIntensity = m_MinIntensity;
			fireLightComponent.m_MaxIntensity = m_MaxIntensity;
			fireLightComponent.m_IntensitySmoothing = m_IntensitySmoothing;
			fireLightComponent.m_MaxPositionOffset = m_MaxPositionOffset;
			fireLightComponent.m_PositionSmoothing = m_PositionSmoothing;
			fireLightComponent.m_Light = m_Light;
			fireLightComponent.m_TargetIntensity = m_TargetIntensity;
			fireLightComponent.m_DefaultPos = m_DefaultPos;
			fireLightComponent.m_TargetPos = m_TargetPos;
			return ref fireLightComponent;
		}

		public static void DelFireLight(this ProtoEntity entity)
			=> s_GameAspect.FireLight.Del(entity);

		//FlickeringLight
		public static bool HasFlickeringLight(this ProtoEntity entity) =>
			s_GameAspect.FlickeringLight.Has(entity);

		public static ref FlickeringLightComponent GetFlickeringLight(this ProtoEntity entity) =>
			ref s_GameAspect.FlickeringLight.Get(entity);

		public static void ReplaceFlickeringLight(this ProtoEntity entity, float m_MinFlickeringTime, float m_MaxFlickeringTime, float m_MinIntensity, float m_MaxIntensity, float m_IntensitySmoothing, Light m_Light, float m_TargetIntensity)
		{
			ref FlickeringLightComponent flickeringLightComponent = ref s_GameAspect.FlickeringLight.Get(entity);
			flickeringLightComponent.m_MinFlickeringTime = m_MinFlickeringTime;
			flickeringLightComponent.m_MaxFlickeringTime = m_MaxFlickeringTime;
			flickeringLightComponent.m_MinIntensity = m_MinIntensity;
			flickeringLightComponent.m_MaxIntensity = m_MaxIntensity;
			flickeringLightComponent.m_IntensitySmoothing = m_IntensitySmoothing;
			flickeringLightComponent.m_Light = m_Light;
			flickeringLightComponent.m_TargetIntensity = m_TargetIntensity;
		}

		public static ref FlickeringLightComponent AddFlickeringLight(this ProtoEntity entity, float m_MinFlickeringTime, float m_MaxFlickeringTime, float m_MinIntensity, float m_MaxIntensity, float m_IntensitySmoothing, Light m_Light, float m_TargetIntensity)
		{
			ref FlickeringLightComponent flickeringLightComponent = ref s_GameAspect.FlickeringLight.Add(entity);
			flickeringLightComponent.m_MinFlickeringTime = m_MinFlickeringTime;
			flickeringLightComponent.m_MaxFlickeringTime = m_MaxFlickeringTime;
			flickeringLightComponent.m_MinIntensity = m_MinIntensity;
			flickeringLightComponent.m_MaxIntensity = m_MaxIntensity;
			flickeringLightComponent.m_IntensitySmoothing = m_IntensitySmoothing;
			flickeringLightComponent.m_Light = m_Light;
			flickeringLightComponent.m_TargetIntensity = m_TargetIntensity;
			return ref flickeringLightComponent;
		}

		public static void DelFlickeringLight(this ProtoEntity entity)
			=> s_GameAspect.FlickeringLight.Del(entity);

		//Light
		public static bool HasLight(this ProtoEntity entity) =>
			s_GameAspect.Light.Has(entity);

		public static ref LightComponent GetLight(this ProtoEntity entity) =>
			ref s_GameAspect.Light.Get(entity);

		public static void ReplaceLight(this ProtoEntity entity, Light m_Light, float m_Range, float m_Intensity, Color m_Color, bool m_RequestShadow, float m_ShadowStrength, LightShadows m_ShadowMode, LightShadowResolution m_ShadowResolution)
		{
			ref LightComponent lightComponent = ref s_GameAspect.Light.Get(entity);
			lightComponent.m_Light = m_Light;
			lightComponent.m_Range = m_Range;
			lightComponent.m_Intensity = m_Intensity;
			lightComponent.m_Color = m_Color;
			lightComponent.m_RequestShadow = m_RequestShadow;
			lightComponent.m_ShadowStrength = m_ShadowStrength;
			lightComponent.m_ShadowMode = m_ShadowMode;
			lightComponent.m_ShadowResolution = m_ShadowResolution;
		}

		public static ref LightComponent AddLight(this ProtoEntity entity, Light m_Light, float m_Range, float m_Intensity, Color m_Color, bool m_RequestShadow, float m_ShadowStrength, LightShadows m_ShadowMode, LightShadowResolution m_ShadowResolution)
		{
			ref LightComponent lightComponent = ref s_GameAspect.Light.Add(entity);
			lightComponent.m_Light = m_Light;
			lightComponent.m_Range = m_Range;
			lightComponent.m_Intensity = m_Intensity;
			lightComponent.m_Color = m_Color;
			lightComponent.m_RequestShadow = m_RequestShadow;
			lightComponent.m_ShadowStrength = m_ShadowStrength;
			lightComponent.m_ShadowMode = m_ShadowMode;
			lightComponent.m_ShadowResolution = m_ShadowResolution;
			return ref lightComponent;
		}

		public static void DelLight(this ProtoEntity entity)
			=> s_GameAspect.Light.Del(entity);

		//PeriodicLight
		public static bool HasPeriodicLight(this ProtoEntity entity) =>
			s_GameAspect.PeriodicLight.Has(entity);

		public static ref PeriodicLightComponent GetPeriodicLight(this ProtoEntity entity) =>
			ref s_GameAspect.PeriodicLight.Get(entity);

		public static void ReplacePeriodicLight(this ProtoEntity entity, Light m_Light, float m_MinIntensity, float m_MaxIntensity, float m_Period, float m_MaxTimeOffset, float m_Offset)
		{
			ref PeriodicLightComponent periodicLightComponent = ref s_GameAspect.PeriodicLight.Get(entity);
			periodicLightComponent.m_Light = m_Light;
			periodicLightComponent.m_MinIntensity = m_MinIntensity;
			periodicLightComponent.m_MaxIntensity = m_MaxIntensity;
			periodicLightComponent.m_Period = m_Period;
			periodicLightComponent.m_MaxTimeOffset = m_MaxTimeOffset;
			periodicLightComponent.m_Offset = m_Offset;
		}

		public static ref PeriodicLightComponent AddPeriodicLight(this ProtoEntity entity, Light m_Light, float m_MinIntensity, float m_MaxIntensity, float m_Period, float m_MaxTimeOffset, float m_Offset)
		{
			ref PeriodicLightComponent periodicLightComponent = ref s_GameAspect.PeriodicLight.Add(entity);
			periodicLightComponent.m_Light = m_Light;
			periodicLightComponent.m_MinIntensity = m_MinIntensity;
			periodicLightComponent.m_MaxIntensity = m_MaxIntensity;
			periodicLightComponent.m_Period = m_Period;
			periodicLightComponent.m_MaxTimeOffset = m_MaxTimeOffset;
			periodicLightComponent.m_Offset = m_Offset;
			return ref periodicLightComponent;
		}

		public static void DelPeriodicLight(this ProtoEntity entity)
			=> s_GameAspect.PeriodicLight.Del(entity);

		//ShadowController
		public static bool HasShadowController(this ProtoEntity entity) =>
			s_GameAspect.ShadowController.Has(entity);

		public static ref ShadowControllerComponent GetShadowController(this ProtoEntity entity) =>
			ref s_GameAspect.ShadowController.Get(entity);

		public static void ReplaceShadowController(this ProtoEntity entity, ImportanceMode m_Importance, DistanceMode m_DistanceMode, float m_LightRangeShadowDistanceCoeff, float m_CustomMaxShadowDistance, IntensityReductionMode m_IntensityReductionMode, float m_CustomIntensityReductionCoeff, RangeReductionMode m_RangeReductionMode, float m_CustomRangeReductionCoeff, bool m_IsShadowEnabled, bool m_IsResolutionReduced)
		{
			ref ShadowControllerComponent shadowControllerComponent = ref s_GameAspect.ShadowController.Get(entity);
			shadowControllerComponent.m_Importance = m_Importance;
			shadowControllerComponent.m_DistanceMode = m_DistanceMode;
			shadowControllerComponent.m_LightRangeShadowDistanceCoeff = m_LightRangeShadowDistanceCoeff;
			shadowControllerComponent.m_CustomMaxShadowDistance = m_CustomMaxShadowDistance;
			shadowControllerComponent.m_IntensityReductionMode = m_IntensityReductionMode;
			shadowControllerComponent.m_CustomIntensityReductionCoeff = m_CustomIntensityReductionCoeff;
			shadowControllerComponent.m_RangeReductionMode = m_RangeReductionMode;
			shadowControllerComponent.m_CustomRangeReductionCoeff = m_CustomRangeReductionCoeff;
			shadowControllerComponent.m_IsShadowEnabled = m_IsShadowEnabled;
			shadowControllerComponent.m_IsResolutionReduced = m_IsResolutionReduced;
		}

		public static ref ShadowControllerComponent AddShadowController(this ProtoEntity entity, ImportanceMode m_Importance, DistanceMode m_DistanceMode, float m_LightRangeShadowDistanceCoeff, float m_CustomMaxShadowDistance, IntensityReductionMode m_IntensityReductionMode, float m_CustomIntensityReductionCoeff, RangeReductionMode m_RangeReductionMode, float m_CustomRangeReductionCoeff, bool m_IsShadowEnabled, bool m_IsResolutionReduced)
		{
			ref ShadowControllerComponent shadowControllerComponent = ref s_GameAspect.ShadowController.Add(entity);
			shadowControllerComponent.m_Importance = m_Importance;
			shadowControllerComponent.m_DistanceMode = m_DistanceMode;
			shadowControllerComponent.m_LightRangeShadowDistanceCoeff = m_LightRangeShadowDistanceCoeff;
			shadowControllerComponent.m_CustomMaxShadowDistance = m_CustomMaxShadowDistance;
			shadowControllerComponent.m_IntensityReductionMode = m_IntensityReductionMode;
			shadowControllerComponent.m_CustomIntensityReductionCoeff = m_CustomIntensityReductionCoeff;
			shadowControllerComponent.m_RangeReductionMode = m_RangeReductionMode;
			shadowControllerComponent.m_CustomRangeReductionCoeff = m_CustomRangeReductionCoeff;
			shadowControllerComponent.m_IsShadowEnabled = m_IsShadowEnabled;
			shadowControllerComponent.m_IsResolutionReduced = m_IsResolutionReduced;
			return ref shadowControllerComponent;
		}

		public static void DelShadowController(this ProtoEntity entity)
			=> s_GameAspect.ShadowController.Del(entity);

		//KillEnemyCounter
		public static bool HasKillEnemyCounter(this ProtoEntity entity) =>
			s_GameAspect.KillEnemyCounter.Has(entity);

		public static ref KillEnemyCounterComponent GetKillEnemyCounter(this ProtoEntity entity) =>
			ref s_GameAspect.KillEnemyCounter.Get(entity);

		public static void ReplaceKillEnemyCounter(this ProtoEntity entity, int value)
		{
			ref KillEnemyCounterComponent killEnemyCounterComponent = ref s_GameAspect.KillEnemyCounter.Get(entity);
			killEnemyCounterComponent.Value = value;
		}

		public static ref KillEnemyCounterComponent AddKillEnemyCounter(this ProtoEntity entity, int value)
		{
			ref KillEnemyCounterComponent killEnemyCounterComponent = ref s_GameAspect.KillEnemyCounter.Add(entity);
			killEnemyCounterComponent.Value = value;
			return ref killEnemyCounterComponent;
		}

		public static void DelKillEnemyCounter(this ProtoEntity entity)
			=> s_GameAspect.KillEnemyCounter.Del(entity);

		//Direction
		public static bool HasDirection(this ProtoEntity entity) =>
			s_GameAspect.Direction.Has(entity);

		public static ref DirectionComponent GetDirection(this ProtoEntity entity) =>
			ref s_GameAspect.Direction.Get(entity);

		public static void ReplaceDirection(this ProtoEntity entity, Vector3 value)
		{
			ref DirectionComponent directionComponent = ref s_GameAspect.Direction.Get(entity);
			directionComponent.Value = value;
		}

		public static ref DirectionComponent AddDirection(this ProtoEntity entity, Vector3 value)
		{
			ref DirectionComponent directionComponent = ref s_GameAspect.Direction.Add(entity);
			directionComponent.Value = value;
			return ref directionComponent;
		}

		public static void DelDirection(this ProtoEntity entity)
			=> s_GameAspect.Direction.Del(entity);

		//Input
		public static bool HasInput(this ProtoEntity entity) =>
			s_GameAspect.Input.Has(entity);

		public static ref InputTag AddInput(this ProtoEntity entity)
		{
			ref InputTag inputTag = ref s_GameAspect.Input.Add(entity);
			return ref inputTag;
		}

		public static void DelInput(this ProtoEntity entity)
			=> s_GameAspect.Input.Del(entity);

		//BehaviourTreeOwner
		public static bool HasBehaviourTreeOwner(this ProtoEntity entity) =>
			s_GameAspect.BehaviourTreeOwner.Has(entity);

		public static ref BehaviourTreeOwnerComponent GetBehaviourTreeOwner(this ProtoEntity entity) =>
			ref s_GameAspect.BehaviourTreeOwner.Get(entity);

		public static void ReplaceBehaviourTreeOwner(this ProtoEntity entity, BehaviourTreeOwner value)
		{
			ref BehaviourTreeOwnerComponent behaviourTreeOwnerComponent = ref s_GameAspect.BehaviourTreeOwner.Get(entity);
			behaviourTreeOwnerComponent.Value = value;
		}

		public static ref BehaviourTreeOwnerComponent AddBehaviourTreeOwner(this ProtoEntity entity, BehaviourTreeOwner value)
		{
			ref BehaviourTreeOwnerComponent behaviourTreeOwnerComponent = ref s_GameAspect.BehaviourTreeOwner.Add(entity);
			behaviourTreeOwnerComponent.Value = value;
			return ref behaviourTreeOwnerComponent;
		}

		public static void DelBehaviourTreeOwner(this ProtoEntity entity)
			=> s_GameAspect.BehaviourTreeOwner.Del(entity);

		//FsmOwner
		public static bool HasFsmOwner(this ProtoEntity entity) =>
			s_GameAspect.FsmOwner.Has(entity);

		public static ref FsmOwnerComponent GetFsmOwner(this ProtoEntity entity) =>
			ref s_GameAspect.FsmOwner.Get(entity);

		public static void ReplaceFsmOwner(this ProtoEntity entity, FSMOwner value)
		{
			ref FsmOwnerComponent fsmOwnerComponent = ref s_GameAspect.FsmOwner.Get(entity);
			fsmOwnerComponent.Value = value;
		}

		public static ref FsmOwnerComponent AddFsmOwner(this ProtoEntity entity, FSMOwner value)
		{
			ref FsmOwnerComponent fsmOwnerComponent = ref s_GameAspect.FsmOwner.Add(entity);
			fsmOwnerComponent.Value = value;
			return ref fsmOwnerComponent;
		}

		public static void DelFsmOwner(this ProtoEntity entity)
			=> s_GameAspect.FsmOwner.Del(entity);

		//Active
		public static bool HasActive(this ProtoEntity entity) =>
			s_GameAspect.Active.Has(entity);

		public static ref ActiveComponent AddActive(this ProtoEntity entity)
		{
			ref ActiveComponent activeComponent = ref s_GameAspect.Active.Add(entity);
			return ref activeComponent;
		}

		public static void DelActive(this ProtoEntity entity)
			=> s_GameAspect.Active.Del(entity);

		//DisableGameObjectEvent
		public static bool HasDisableGameObjectEvent(this ProtoEntity entity) =>
			s_GameAspect.DisableGameObjectEvent.Has(entity);

		public static ref DisableGameObjectEvent AddDisableGameObjectEvent(this ProtoEntity entity)
		{
			ref DisableGameObjectEvent disableGameObjectEvent = ref s_GameAspect.DisableGameObjectEvent.Add(entity);
			return ref disableGameObjectEvent;
		}

		public static void DelDisableGameObjectEvent(this ProtoEntity entity)
			=> s_GameAspect.DisableGameObjectEvent.Del(entity);

		//EnableGameObjectEvent
		public static bool HasEnableGameObjectEvent(this ProtoEntity entity) =>
			s_GameAspect.EnableGameObjectEvent.Has(entity);

		public static ref EnableGameObjectEvent AddEnableGameObjectEvent(this ProtoEntity entity)
		{
			ref EnableGameObjectEvent enableGameObjectEvent = ref s_GameAspect.EnableGameObjectEvent.Add(entity);
			return ref enableGameObjectEvent;
		}

		public static void DelEnableGameObjectEvent(this ProtoEntity entity)
			=> s_GameAspect.EnableGameObjectEvent.Del(entity);

		//GameObject
		public static bool HasGameObject(this ProtoEntity entity) =>
			s_GameAspect.GameObject.Has(entity);

		public static ref GameObjectComponent GetGameObject(this ProtoEntity entity) =>
			ref s_GameAspect.GameObject.Get(entity);

		public static void ReplaceGameObject(this ProtoEntity entity, GameObject value)
		{
			ref GameObjectComponent gameObjectComponent = ref s_GameAspect.GameObject.Get(entity);
			gameObjectComponent.Value = value;
		}

		public static ref GameObjectComponent AddGameObject(this ProtoEntity entity, GameObject value)
		{
			ref GameObjectComponent gameObjectComponent = ref s_GameAspect.GameObject.Add(entity);
			gameObjectComponent.Value = value;
			return ref gameObjectComponent;
		}

		public static void DelGameObject(this ProtoEntity entity)
			=> s_GameAspect.GameObject.Del(entity);

		//ExplosionBodyBloody
		public static bool HasExplosionBodyBloody(this ProtoEntity entity) =>
			s_GameAspect.ExplosionBodyBloody.Has(entity);

		public static ref ExplosionBodyBloodyTag AddExplosionBodyBloody(this ProtoEntity entity)
		{
			ref ExplosionBodyBloodyTag explosionBodyBloodyTag = ref s_GameAspect.ExplosionBodyBloody.Add(entity);
			return ref explosionBodyBloodyTag;
		}

		public static void DelExplosionBodyBloody(this ProtoEntity entity)
			=> s_GameAspect.ExplosionBodyBloody.Del(entity);

		//ExplosionBody
		public static bool HasExplosionBody(this ProtoEntity entity) =>
			s_GameAspect.ExplosionBody.Has(entity);

		public static ref ExplosionBodyTag AddExplosionBody(this ProtoEntity entity)
		{
			ref ExplosionBodyTag explosionBodyTag = ref s_GameAspect.ExplosionBody.Add(entity);
			return ref explosionBodyTag;
		}

		public static void DelExplosionBody(this ProtoEntity entity)
			=> s_GameAspect.ExplosionBody.Del(entity);

		//BloodParticle
		public static bool HasBloodParticle(this ProtoEntity entity) =>
			s_GameAspect.BloodParticle.Has(entity);

		public static ref BloodParticleComponent GetBloodParticle(this ProtoEntity entity) =>
			ref s_GameAspect.BloodParticle.Get(entity);

		public static void ReplaceBloodParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref BloodParticleComponent bloodParticleComponent = ref s_GameAspect.BloodParticle.Get(entity);
			bloodParticleComponent.Value = value;
		}

		public static ref BloodParticleComponent AddBloodParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref BloodParticleComponent bloodParticleComponent = ref s_GameAspect.BloodParticle.Add(entity);
			bloodParticleComponent.Value = value;
			return ref bloodParticleComponent;
		}

		public static void DelBloodParticle(this ProtoEntity entity)
			=> s_GameAspect.BloodParticle.Del(entity);

		//DamageEvent
		public static bool HasDamageEvent(this ProtoEntity entity) =>
			s_GameAspect.DamageEvent.Has(entity);

		public static ref DamageEvent GetDamageEvent(this ProtoEntity entity) =>
			ref s_GameAspect.DamageEvent.Get(entity);

		public static void ReplaceDamageEvent(this ProtoEntity entity, int value)
		{
			ref DamageEvent damageEvent = ref s_GameAspect.DamageEvent.Get(entity);
			damageEvent.Value = value;
		}

		public static ref DamageEvent AddDamageEvent(this ProtoEntity entity, int value)
		{
			ref DamageEvent damageEvent = ref s_GameAspect.DamageEvent.Add(entity);
			damageEvent.Value = value;
			return ref damageEvent;
		}

		public static void DelDamageEvent(this ProtoEntity entity)
			=> s_GameAspect.DamageEvent.Del(entity);

		//DamageTimer
		public static bool HasDamageTimer(this ProtoEntity entity) =>
			s_GameAspect.DamageTimer.Has(entity);

		public static ref DamageTimerComponent GetDamageTimer(this ProtoEntity entity) =>
			ref s_GameAspect.DamageTimer.Get(entity);

		public static void ReplaceDamageTimer(this ProtoEntity entity, float value)
		{
			ref DamageTimerComponent damageTimerComponent = ref s_GameAspect.DamageTimer.Get(entity);
			damageTimerComponent.Value = value;
		}

		public static ref DamageTimerComponent AddDamageTimer(this ProtoEntity entity, float value)
		{
			ref DamageTimerComponent damageTimerComponent = ref s_GameAspect.DamageTimer.Add(entity);
			damageTimerComponent.Value = value;
			return ref damageTimerComponent;
		}

		public static void DelDamageTimer(this ProtoEntity entity)
			=> s_GameAspect.DamageTimer.Del(entity);

		//DeadEvent
		public static bool HasDeadEvent(this ProtoEntity entity) =>
			s_GameAspect.DeadEvent.Has(entity);

		public static ref DeadEvent AddDeadEvent(this ProtoEntity entity)
		{
			ref DeadEvent deadEvent = ref s_GameAspect.DeadEvent.Add(entity);
			return ref deadEvent;
		}

		public static void DelDeadEvent(this ProtoEntity entity)
			=> s_GameAspect.DeadEvent.Del(entity);

		//HealthBar
		public static bool HasHealthBar(this ProtoEntity entity) =>
			s_GameAspect.HealthBar.Has(entity);

		public static ref HealthBarComponent GetHealthBar(this ProtoEntity entity) =>
			ref s_GameAspect.HealthBar.Get(entity);

		public static void ReplaceHealthBar(this ProtoEntity entity, Image value)
		{
			ref HealthBarComponent healthBarComponent = ref s_GameAspect.HealthBar.Get(entity);
			healthBarComponent.Value = value;
		}

		public static ref HealthBarComponent AddHealthBar(this ProtoEntity entity, Image value)
		{
			ref HealthBarComponent healthBarComponent = ref s_GameAspect.HealthBar.Add(entity);
			healthBarComponent.Value = value;
			return ref healthBarComponent;
		}

		public static void DelHealthBar(this ProtoEntity entity)
			=> s_GameAspect.HealthBar.Del(entity);

		//Health
		public static bool HasHealth(this ProtoEntity entity) =>
			s_GameAspect.Health.Has(entity);

		public static ref HealthComponent GetHealth(this ProtoEntity entity) =>
			ref s_GameAspect.Health.Get(entity);

		public static void ReplaceHealth(this ProtoEntity entity, int value)
		{
			ref HealthComponent healthComponent = ref s_GameAspect.Health.Get(entity);
			healthComponent.Value = value;
		}

		public static ref HealthComponent AddHealth(this ProtoEntity entity, int value)
		{
			ref HealthComponent healthComponent = ref s_GameAspect.Health.Add(entity);
			healthComponent.Value = value;
			return ref healthComponent;
		}

		public static void DelHealth(this ProtoEntity entity)
			=> s_GameAspect.Health.Del(entity);

		//HealthText
		public static bool HasHealthText(this ProtoEntity entity) =>
			s_GameAspect.HealthText.Has(entity);

		public static ref HealthTextComponent GetHealthText(this ProtoEntity entity) =>
			ref s_GameAspect.HealthText.Get(entity);

		public static void ReplaceHealthText(this ProtoEntity entity, TMP_Text value)
		{
			ref HealthTextComponent healthTextComponent = ref s_GameAspect.HealthText.Get(entity);
			healthTextComponent.Value = value;
		}

		public static ref HealthTextComponent AddHealthText(this ProtoEntity entity, TMP_Text value)
		{
			ref HealthTextComponent healthTextComponent = ref s_GameAspect.HealthText.Add(entity);
			healthTextComponent.Value = value;
			return ref healthTextComponent;
		}

		public static void DelHealthText(this ProtoEntity entity)
			=> s_GameAspect.HealthText.Del(entity);

		//MaxHealth
		public static bool HasMaxHealth(this ProtoEntity entity) =>
			s_GameAspect.MaxHealth.Has(entity);

		public static ref MaxHealthComponent GetMaxHealth(this ProtoEntity entity) =>
			ref s_GameAspect.MaxHealth.Get(entity);

		public static void ReplaceMaxHealth(this ProtoEntity entity, int value)
		{
			ref MaxHealthComponent maxHealthComponent = ref s_GameAspect.MaxHealth.Get(entity);
			maxHealthComponent.Value = value;
		}

		public static ref MaxHealthComponent AddMaxHealth(this ProtoEntity entity, int value)
		{
			ref MaxHealthComponent maxHealthComponent = ref s_GameAspect.MaxHealth.Add(entity);
			maxHealthComponent.Value = value;
			return ref maxHealthComponent;
		}

		public static void DelMaxHealth(this ProtoEntity entity)
			=> s_GameAspect.MaxHealth.Del(entity);

		//ObstacleCollisionEvent
		public static bool HasObstacleCollisionEvent(this ProtoEntity entity) =>
			s_GameAspect.ObstacleCollisionEvent.Has(entity);

		public static ref ObstacleCollisionEvent GetObstacleCollisionEvent(this ProtoEntity entity) =>
			ref s_GameAspect.ObstacleCollisionEvent.Get(entity);

		public static void ReplaceObstacleCollisionEvent(this ProtoEntity entity, ControllerColliderHit hit, ProtoEntity obstacle)
		{
			ref ObstacleCollisionEvent obstacleCollisionEvent = ref s_GameAspect.ObstacleCollisionEvent.Get(entity);
			obstacleCollisionEvent.Hit = hit;
			obstacleCollisionEvent.Obstacle = obstacle;
		}

		public static ref ObstacleCollisionEvent AddObstacleCollisionEvent(this ProtoEntity entity, ControllerColliderHit hit, ProtoEntity obstacle)
		{
			ref ObstacleCollisionEvent obstacleCollisionEvent = ref s_GameAspect.ObstacleCollisionEvent.Add(entity);
			obstacleCollisionEvent.Hit = hit;
			obstacleCollisionEvent.Obstacle = obstacle;
			return ref obstacleCollisionEvent;
		}

		public static void DelObstacleCollisionEvent(this ProtoEntity entity)
			=> s_GameAspect.ObstacleCollisionEvent.Del(entity);

		//ApplyDailyRewardEvent
		public static bool HasApplyDailyRewardEvent(this ProtoEntity entity) =>
			s_GameAspect.ApplyDailyRewardEvent.Has(entity);

		public static ref ApplyDailyRewardEvent AddApplyDailyRewardEvent(this ProtoEntity entity)
		{
			ref ApplyDailyRewardEvent applyDailyRewardEvent = ref s_GameAspect.ApplyDailyRewardEvent.Add(entity);
			return ref applyDailyRewardEvent;
		}

		public static void DelApplyDailyRewardEvent(this ProtoEntity entity)
			=> s_GameAspect.ApplyDailyRewardEvent.Del(entity);

		//DailyRewardData
		public static bool HasDailyRewardData(this ProtoEntity entity) =>
			s_GameAspect.DailyRewardData.Has(entity);

		public static ref DailyRewardDataComponent GetDailyRewardData(this ProtoEntity entity) =>
			ref s_GameAspect.DailyRewardData.Get(entity);

		public static void ReplaceDailyRewardData(this ProtoEntity entity, DateTime lastRewardTime, TimeSpan currentTime, DateTime targetRewardTime, DateTime serverTime)
		{
			ref DailyRewardDataComponent dailyRewardDataComponent = ref s_GameAspect.DailyRewardData.Get(entity);
			dailyRewardDataComponent.LastRewardTime = lastRewardTime;
			dailyRewardDataComponent.CurrentTime = currentTime;
			dailyRewardDataComponent.TargetRewardTime = targetRewardTime;
			dailyRewardDataComponent.ServerTime = serverTime;
		}

		public static ref DailyRewardDataComponent AddDailyRewardData(this ProtoEntity entity, DateTime lastRewardTime, TimeSpan currentTime, DateTime targetRewardTime, DateTime serverTime)
		{
			ref DailyRewardDataComponent dailyRewardDataComponent = ref s_GameAspect.DailyRewardData.Add(entity);
			dailyRewardDataComponent.LastRewardTime = lastRewardTime;
			dailyRewardDataComponent.CurrentTime = currentTime;
			dailyRewardDataComponent.TargetRewardTime = targetRewardTime;
			dailyRewardDataComponent.ServerTime = serverTime;
			return ref dailyRewardDataComponent;
		}

		public static void DelDailyRewardData(this ProtoEntity entity)
			=> s_GameAspect.DailyRewardData.Del(entity);

		//DailyRewardModule
		public static bool HasDailyRewardModule(this ProtoEntity entity) =>
			s_GameAspect.DailyRewardModule.Has(entity);

		public static ref DailyRewardModuleComponent GetDailyRewardModule(this ProtoEntity entity) =>
			ref s_GameAspect.DailyRewardModule.Get(entity);

		public static void ReplaceDailyRewardModule(this ProtoEntity entity, DailyRewardModule value)
		{
			ref DailyRewardModuleComponent dailyRewardModuleComponent = ref s_GameAspect.DailyRewardModule.Get(entity);
			dailyRewardModuleComponent.Value = value;
		}

		public static ref DailyRewardModuleComponent AddDailyRewardModule(this ProtoEntity entity, DailyRewardModule value)
		{
			ref DailyRewardModuleComponent dailyRewardModuleComponent = ref s_GameAspect.DailyRewardModule.Add(entity);
			dailyRewardModuleComponent.Value = value;
			return ref dailyRewardModuleComponent;
		}

		public static void DelDailyRewardModule(this ProtoEntity entity)
			=> s_GameAspect.DailyRewardModule.Del(entity);

		//DailyReward
		public static bool HasDailyReward(this ProtoEntity entity) =>
			s_GameAspect.DailyReward.Has(entity);

		public static ref DailyRewardTag AddDailyReward(this ProtoEntity entity)
		{
			ref DailyRewardTag dailyRewardTag = ref s_GameAspect.DailyReward.Add(entity);
			return ref dailyRewardTag;
		}

		public static void DelDailyReward(this ProtoEntity entity)
			=> s_GameAspect.DailyReward.Del(entity);

		//AttackPower
		public static bool HasAttackPower(this ProtoEntity entity) =>
			s_GameAspect.AttackPower.Has(entity);

		public static ref AttackPowerComponent GetAttackPower(this ProtoEntity entity) =>
			ref s_GameAspect.AttackPower.Get(entity);

		public static void ReplaceAttackPower(this ProtoEntity entity, int value)
		{
			ref AttackPowerComponent attackPowerComponent = ref s_GameAspect.AttackPower.Get(entity);
			attackPowerComponent.Value = value;
		}

		public static ref AttackPowerComponent AddAttackPower(this ProtoEntity entity, int value)
		{
			ref AttackPowerComponent attackPowerComponent = ref s_GameAspect.AttackPower.Add(entity);
			attackPowerComponent.Value = value;
			return ref attackPowerComponent;
		}

		public static void DelAttackPower(this ProtoEntity entity)
			=> s_GameAspect.AttackPower.Del(entity);

		//Available
		public static bool HasAvailable(this ProtoEntity entity) =>
			s_GameAspect.Available.Has(entity);

		public static ref AvailableComponent AddAvailable(this ProtoEntity entity)
		{
			ref AvailableComponent availableComponent = ref s_GameAspect.Available.Add(entity);
			return ref availableComponent;
		}

		public static void DelAvailable(this ProtoEntity entity)
			=> s_GameAspect.Available.Del(entity);

		//Complete
		public static bool HasComplete(this ProtoEntity entity) =>
			s_GameAspect.Complete.Has(entity);

		public static ref CompleteComponent AddComplete(this ProtoEntity entity)
		{
			ref CompleteComponent completeComponent = ref s_GameAspect.Complete.Add(entity);
			return ref completeComponent;
		}

		public static void DelComplete(this ProtoEntity entity)
			=> s_GameAspect.Complete.Del(entity);

		//DecreaseEvent
		public static bool HasDecreaseEvent(this ProtoEntity entity) =>
			s_GameAspect.DecreaseEvent.Has(entity);

		public static ref DecreaseEvent AddDecreaseEvent(this ProtoEntity entity)
		{
			ref DecreaseEvent decreaseEvent = ref s_GameAspect.DecreaseEvent.Add(entity);
			return ref decreaseEvent;
		}

		public static void DelDecreaseEvent(this ProtoEntity entity)
			=> s_GameAspect.DecreaseEvent.Del(entity);

		//Distance
		public static bool HasDistance(this ProtoEntity entity) =>
			s_GameAspect.Distance.Has(entity);

		public static ref DistanceComponent GetDistance(this ProtoEntity entity) =>
			ref s_GameAspect.Distance.Get(entity);

		public static void ReplaceDistance(this ProtoEntity entity, float value)
		{
			ref DistanceComponent distanceComponent = ref s_GameAspect.Distance.Get(entity);
			distanceComponent.Value = value;
		}

		public static ref DistanceComponent AddDistance(this ProtoEntity entity, float value)
		{
			ref DistanceComponent distanceComponent = ref s_GameAspect.Distance.Add(entity);
			distanceComponent.Value = value;
			return ref distanceComponent;
		}

		public static void DelDistance(this ProtoEntity entity)
			=> s_GameAspect.Distance.Del(entity);

		//EntityLink
		public static bool HasEntityLink(this ProtoEntity entity) =>
			s_GameAspect.EntityLink.Has(entity);

		public static ref EntityLinkComponent GetEntityLink(this ProtoEntity entity) =>
			ref s_GameAspect.EntityLink.Get(entity);

		public static void ReplaceEntityLink(this ProtoEntity entity, EntityLink entityLink, int entityId, ProtoEntity protoEntity)
		{
			ref EntityLinkComponent entityLinkComponent = ref s_GameAspect.EntityLink.Get(entity);
			entityLinkComponent.EntityLink = entityLink;
			entityLinkComponent.EntityId = entityId;
			entityLinkComponent.ProtoEntity = protoEntity;
		}

		public static ref EntityLinkComponent AddEntityLink(this ProtoEntity entity, EntityLink entityLink, int entityId, ProtoEntity protoEntity)
		{
			ref EntityLinkComponent entityLinkComponent = ref s_GameAspect.EntityLink.Add(entity);
			entityLinkComponent.EntityLink = entityLink;
			entityLinkComponent.EntityId = entityId;
			entityLinkComponent.ProtoEntity = protoEntity;
			return ref entityLinkComponent;
		}

		public static void DelEntityLink(this ProtoEntity entity)
			=> s_GameAspect.EntityLink.Del(entity);

		//HeadTransform
		public static bool HasHeadTransform(this ProtoEntity entity) =>
			s_GameAspect.HeadTransform.Has(entity);

		public static ref HeadTransformComponent GetHeadTransform(this ProtoEntity entity) =>
			ref s_GameAspect.HeadTransform.Get(entity);

		public static void ReplaceHeadTransform(this ProtoEntity entity, Transform value)
		{
			ref HeadTransformComponent headTransformComponent = ref s_GameAspect.HeadTransform.Get(entity);
			headTransformComponent.Value = value;
		}

		public static ref HeadTransformComponent AddHeadTransform(this ProtoEntity entity, Transform value)
		{
			ref HeadTransformComponent headTransformComponent = ref s_GameAspect.HeadTransform.Add(entity);
			headTransformComponent.Value = value;
			return ref headTransformComponent;
		}

		public static void DelHeadTransform(this ProtoEntity entity)
			=> s_GameAspect.HeadTransform.Del(entity);

		//IncreaseEvent
		public static bool HasIncreaseEvent(this ProtoEntity entity) =>
			s_GameAspect.IncreaseEvent.Has(entity);

		public static ref IncreaseEvent AddIncreaseEvent(this ProtoEntity entity)
		{
			ref IncreaseEvent increaseEvent = ref s_GameAspect.IncreaseEvent.Add(entity);
			return ref increaseEvent;
		}

		public static void DelIncreaseEvent(this ProtoEntity entity)
			=> s_GameAspect.IncreaseEvent.Del(entity);

		//Initialized
		public static bool HasInitialized(this ProtoEntity entity) =>
			s_GameAspect.Initialized.Has(entity);

		public static ref InitializedComponent AddInitialized(this ProtoEntity entity)
		{
			ref InitializedComponent initializedComponent = ref s_GameAspect.Initialized.Add(entity);
			return ref initializedComponent;
		}

		public static void DelInitialized(this ProtoEntity entity)
			=> s_GameAspect.Initialized.Del(entity);

		//InitializeEvent
		public static bool HasInitializeEvent(this ProtoEntity entity) =>
			s_GameAspect.InitializeEvent.Has(entity);

		public static ref InitializeEvent AddInitializeEvent(this ProtoEntity entity)
		{
			ref InitializeEvent initializeEvent = ref s_GameAspect.InitializeEvent.Add(entity);
			return ref initializeEvent;
		}

		public static void DelInitializeEvent(this ProtoEntity entity)
			=> s_GameAspect.InitializeEvent.Del(entity);

		//InPool
		public static bool HasInPool(this ProtoEntity entity) =>
			s_GameAspect.InPool.Has(entity);

		public static ref InPoolComponent AddInPool(this ProtoEntity entity)
		{
			ref InPoolComponent inPoolComponent = ref s_GameAspect.InPool.Add(entity);
			return ref inPoolComponent;
		}

		public static void DelInPool(this ProtoEntity entity)
			=> s_GameAspect.InPool.Del(entity);

		//Scale
		public static bool HasScale(this ProtoEntity entity) =>
			s_GameAspect.Scale.Has(entity);

		public static ref ScaleComponent GetScale(this ProtoEntity entity) =>
			ref s_GameAspect.Scale.Get(entity);

		public static void ReplaceScale(this ProtoEntity entity, Vector3 targetScale, Vector3 currentScale)
		{
			ref ScaleComponent scaleComponent = ref s_GameAspect.Scale.Get(entity);
			scaleComponent.TargetScale = targetScale;
			scaleComponent.CurrentScale = currentScale;
		}

		public static ref ScaleComponent AddScale(this ProtoEntity entity, Vector3 targetScale, Vector3 currentScale)
		{
			ref ScaleComponent scaleComponent = ref s_GameAspect.Scale.Add(entity);
			scaleComponent.TargetScale = targetScale;
			scaleComponent.CurrentScale = currentScale;
			return ref scaleComponent;
		}

		public static void DelScale(this ProtoEntity entity)
			=> s_GameAspect.Scale.Del(entity);

		//Sequence
		public static bool HasSequence(this ProtoEntity entity) =>
			s_GameAspect.Sequence.Has(entity);

		public static ref SequenceComponent GetSequence(this ProtoEntity entity) =>
			ref s_GameAspect.Sequence.Get(entity);

		public static void ReplaceSequence(this ProtoEntity entity, Sequence value)
		{
			ref SequenceComponent sequenceComponent = ref s_GameAspect.Sequence.Get(entity);
			sequenceComponent.Value = value;
		}

		public static ref SequenceComponent AddSequence(this ProtoEntity entity, Sequence value)
		{
			ref SequenceComponent sequenceComponent = ref s_GameAspect.Sequence.Add(entity);
			sequenceComponent.Value = value;
			return ref sequenceComponent;
		}

		public static void DelSequence(this ProtoEntity entity)
			=> s_GameAspect.Sequence.Del(entity);

		//StringId
		public static bool HasStringId(this ProtoEntity entity) =>
			s_GameAspect.StringId.Has(entity);

		public static ref StringIdComponent GetStringId(this ProtoEntity entity) =>
			ref s_GameAspect.StringId.Get(entity);

		public static void ReplaceStringId(this ProtoEntity entity, String value)
		{
			ref StringIdComponent stringIdComponent = ref s_GameAspect.StringId.Get(entity);
			stringIdComponent.Value = value;
		}

		public static ref StringIdComponent AddStringId(this ProtoEntity entity, String value)
		{
			ref StringIdComponent stringIdComponent = ref s_GameAspect.StringId.Add(entity);
			stringIdComponent.Value = value;
			return ref stringIdComponent;
		}

		public static void DelStringId(this ProtoEntity entity)
			=> s_GameAspect.StringId.Del(entity);

		//Transform
		public static bool HasTransform(this ProtoEntity entity) =>
			s_GameAspect.Transform.Has(entity);

		public static ref TransformComponent GetTransform(this ProtoEntity entity) =>
			ref s_GameAspect.Transform.Get(entity);

		public static void ReplaceTransform(this ProtoEntity entity, Transform value)
		{
			ref TransformComponent transformComponent = ref s_GameAspect.Transform.Get(entity);
			transformComponent.Value = value;
		}

		public static ref TransformComponent AddTransform(this ProtoEntity entity, Transform value)
		{
			ref TransformComponent transformComponent = ref s_GameAspect.Transform.Add(entity);
			transformComponent.Value = value;
			return ref transformComponent;
		}

		public static void DelTransform(this ProtoEntity entity)
			=> s_GameAspect.Transform.Del(entity);

		//CharacterConfig
		public static bool HasCharacterConfig(this ProtoEntity entity) =>
			s_GameAspect.CharacterConfig.Has(entity);

		public static ref CharacterConfigComponent GetCharacterConfig(this ProtoEntity entity) =>
			ref s_GameAspect.CharacterConfig.Get(entity);

		public static void ReplaceCharacterConfig(this ProtoEntity entity, CharacterConfig value)
		{
			ref CharacterConfigComponent characterConfigComponent = ref s_GameAspect.CharacterConfig.Get(entity);
			characterConfigComponent.Value = value;
		}

		public static ref CharacterConfigComponent AddCharacterConfig(this ProtoEntity entity, CharacterConfig value)
		{
			ref CharacterConfigComponent characterConfigComponent = ref s_GameAspect.CharacterConfig.Add(entity);
			characterConfigComponent.Value = value;
			return ref characterConfigComponent;
		}

		public static void DelCharacterConfig(this ProtoEntity entity)
			=> s_GameAspect.CharacterConfig.Del(entity);

		//Character
		public static bool HasCharacter(this ProtoEntity entity) =>
			s_GameAspect.Character.Has(entity);

		public static ref CharacterTag AddCharacter(this ProtoEntity entity)
		{
			ref CharacterTag characterTag = ref s_GameAspect.Character.Add(entity);
			return ref characterTag;
		}

		public static void DelCharacter(this ProtoEntity entity)
			=> s_GameAspect.Character.Del(entity);

		//Camera
		public static bool HasCamera(this ProtoEntity entity) =>
			s_GameAspect.Camera.Has(entity);

		public static ref CameraComponent GetCamera(this ProtoEntity entity) =>
			ref s_GameAspect.Camera.Get(entity);

		public static void ReplaceCamera(this ProtoEntity entity, Camera camera)
		{
			ref CameraComponent cameraComponent = ref s_GameAspect.Camera.Get(entity);
			cameraComponent.Camera = camera;
		}

		public static ref CameraComponent AddCamera(this ProtoEntity entity, Camera camera)
		{
			ref CameraComponent cameraComponent = ref s_GameAspect.Camera.Add(entity);
			cameraComponent.Camera = camera;
			return ref cameraComponent;
		}

		public static void DelCamera(this ProtoEntity entity)
			=> s_GameAspect.Camera.Del(entity);

		//CinemachineCamera
		public static bool HasCinemachineCamera(this ProtoEntity entity) =>
			s_GameAspect.CinemachineCamera.Has(entity);

		public static ref CinemachineCameraComponent GetCinemachineCamera(this ProtoEntity entity) =>
			ref s_GameAspect.CinemachineCamera.Get(entity);

		public static void ReplaceCinemachineCamera(this ProtoEntity entity, CinemachineCamera value)
		{
			ref CinemachineCameraComponent cinemachineCameraComponent = ref s_GameAspect.CinemachineCamera.Get(entity);
			cinemachineCameraComponent.Value = value;
		}

		public static ref CinemachineCameraComponent AddCinemachineCamera(this ProtoEntity entity, CinemachineCamera value)
		{
			ref CinemachineCameraComponent cinemachineCameraComponent = ref s_GameAspect.CinemachineCamera.Add(entity);
			cinemachineCameraComponent.Value = value;
			return ref cinemachineCameraComponent;
		}

		public static void DelCinemachineCamera(this ProtoEntity entity)
			=> s_GameAspect.CinemachineCamera.Del(entity);

		//MainCamera
		public static bool HasMainCamera(this ProtoEntity entity) =>
			s_GameAspect.MainCamera.Has(entity);

		public static ref MainCameraTag AddMainCamera(this ProtoEntity entity)
		{
			ref MainCameraTag mainCameraTag = ref s_GameAspect.MainCamera.Add(entity);
			return ref mainCameraTag;
		}

		public static void DelMainCamera(this ProtoEntity entity)
			=> s_GameAspect.MainCamera.Del(entity);

		//Animator
		public static bool HasAnimator(this ProtoEntity entity) =>
			s_GameAspect.Animator.Has(entity);

		public static ref AnimatorComponent GetAnimator(this ProtoEntity entity) =>
			ref s_GameAspect.Animator.Get(entity);

		public static void ReplaceAnimator(this ProtoEntity entity, Animator value)
		{
			ref AnimatorComponent animatorComponent = ref s_GameAspect.Animator.Get(entity);
			animatorComponent.Value = value;
		}

		public static ref AnimatorComponent AddAnimator(this ProtoEntity entity, Animator value)
		{
			ref AnimatorComponent animatorComponent = ref s_GameAspect.Animator.Add(entity);
			animatorComponent.Value = value;
			return ref animatorComponent;
		}

		public static void DelAnimator(this ProtoEntity entity)
			=> s_GameAspect.Animator.Del(entity);

		//AnimatorLod
		public static bool HasAnimatorLod(this ProtoEntity entity) =>
			s_GameAspect.AnimatorLod.Has(entity);

		public static ref AnimatorLodComponent GetAnimatorLod(this ProtoEntity entity) =>
			ref s_GameAspect.AnimatorLod.Get(entity);

		public static void ReplaceAnimatorLod(this ProtoEntity entity, bool changeSkinWeights, SkinnedMeshRenderer[] skinnedMeshRenderers, int frameCountdown)
		{
			ref AnimatorLodComponent animatorLodComponent = ref s_GameAspect.AnimatorLod.Get(entity);
			animatorLodComponent.ChangeSkinWeights = changeSkinWeights;
			animatorLodComponent.SkinnedMeshRenderers = skinnedMeshRenderers;
			animatorLodComponent.FrameCountdown = frameCountdown;
		}

		public static ref AnimatorLodComponent AddAnimatorLod(this ProtoEntity entity, bool changeSkinWeights, SkinnedMeshRenderer[] skinnedMeshRenderers, int frameCountdown)
		{
			ref AnimatorLodComponent animatorLodComponent = ref s_GameAspect.AnimatorLod.Add(entity);
			animatorLodComponent.ChangeSkinWeights = changeSkinWeights;
			animatorLodComponent.SkinnedMeshRenderers = skinnedMeshRenderers;
			animatorLodComponent.FrameCountdown = frameCountdown;
			return ref animatorLodComponent;
		}

		public static void DelAnimatorLod(this ProtoEntity entity)
			=> s_GameAspect.AnimatorLod.Del(entity);

		//AnimancerEcs
		public static bool HasAnimancerEcs(this ProtoEntity entity) =>
			s_GameAspect.AnimancerEcs.Has(entity);

		public static ref AnimancerEcsComponent GetAnimancerEcs(this ProtoEntity entity) =>
			ref s_GameAspect.AnimancerEcs.Get(entity);

		public static void ReplaceAnimancerEcs(this ProtoEntity entity, AnimancerComponent value)
		{
			ref AnimancerEcsComponent animancerEcsComponent = ref s_GameAspect.AnimancerEcs.Get(entity);
			animancerEcsComponent.Value = value;
		}

		public static ref AnimancerEcsComponent AddAnimancerEcs(this ProtoEntity entity, AnimancerComponent value)
		{
			ref AnimancerEcsComponent animancerEcsComponent = ref s_GameAspect.AnimancerEcs.Add(entity);
			animancerEcsComponent.Value = value;
			return ref animancerEcsComponent;
		}

		public static void DelAnimancerEcs(this ProtoEntity entity)
			=> s_GameAspect.AnimancerEcs.Del(entity);

		//AnimancerState
		public static bool HasAnimancerState(this ProtoEntity entity) =>
			s_GameAspect.AnimancerState.Has(entity);

		public static ref AnimancerStateComponent GetAnimancerState(this ProtoEntity entity) =>
			ref s_GameAspect.AnimancerState.Get(entity);

		public static void ReplaceAnimancerState(this ProtoEntity entity, AnimancerState value)
		{
			ref AnimancerStateComponent animancerStateComponent = ref s_GameAspect.AnimancerState.Get(entity);
			animancerStateComponent.Value = value;
		}

		public static ref AnimancerStateComponent AddAnimancerState(this ProtoEntity entity, AnimancerState value)
		{
			ref AnimancerStateComponent animancerStateComponent = ref s_GameAspect.AnimancerState.Add(entity);
			animancerStateComponent.Value = value;
			return ref animancerStateComponent;
		}

		public static void DelAnimancerState(this ProtoEntity entity)
			=> s_GameAspect.AnimancerState.Del(entity);

	}
}
