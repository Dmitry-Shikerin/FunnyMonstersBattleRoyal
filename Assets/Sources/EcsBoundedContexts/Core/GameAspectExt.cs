using System;
using UnityEngine;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Components;
using System;
using Sources.EcsBoundedContexts.Weapons.Domain;
using Sources.EcsBoundedContexts.Weapons.Presentation;
using Sources.EcsBoundedContexts.Volumes.Domain.Components;
using Sources.EcsBoundedContexts.Volumes.Presentation;
using Sources.EcsBoundedContexts.Volumes.Domain.Enums;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.EcsBoundedContexts.Upgrades.Presentation;
using Sources.EcsBoundedContexts.Upgrades.Domain.Configs;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Tutorials.Domain.Components;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Types;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.PlayerWallets.Presentation;
using Sources.EcsBoundedContexts.Particles.Domain;
using UnityEngine;
using Sources.EcsBoundedContexts.NukeAbilities.Domain;
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
using Sources.EcsBoundedContexts.HealthBoosters.Domain.Components;
using Sources.EcsBoundedContexts.HealthBoosters.Presentation;
using Sources.EcsBoundedContexts.GraphOwners.Domain;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.StateMachines;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Domain;
using Sources.EcsBoundedContexts.ExplosionBodies.Domain;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.EcsBoundedContexts.EnemySpawners.Presentation;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.EcsBoundedContexts.Damage.Domain;
using UnityEngine.UI;
using TMPro;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.EcsBoundedContexts.DailyRewards.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using DG.Tweening;
using Sources.EcsBoundedContexts.CharacterSpawner.Domain;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation.Types;
using System.Collections.Generic;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Unity.Cinemachine;
using Sources.EcsBoundedContexts.BurnAbilities.Domain.Components;
using Sources.EcsBoundedContexts.Bunker.Domain.Components;
using Sources.EcsBoundedContexts.Bunker.Presentation;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.ApplyAbility.Presentation;
using Sources.EcsBoundedContexts.Animators;
using Sources.EcsBoundedContexts.AnimatorLod.Domain.Components;
using Sources.EcsBoundedContexts.Animancers.Domain.Components;
using Animancer;
using Sources.EcsBoundedContexts.Achievements.Domain.Components;
using Sources.EcsBoundedContexts.Achievements.Presentation;

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

		//ApplyUpgradeEvent
		public static bool HasApplyUpgradeEvent(this ProtoEntity entity) =>
			s_GameAspect.ApplyUpgradeEvent.Has(entity);

		public static ref ApplyUpgradeEvent AddApplyUpgradeEvent(this ProtoEntity entity)
		{
			ref ApplyUpgradeEvent applyUpgradeEvent = ref s_GameAspect.ApplyUpgradeEvent.Add(entity);
			return ref applyUpgradeEvent;
		}

		public static void DelApplyUpgradeEvent(this ProtoEntity entity)
			=> s_GameAspect.ApplyUpgradeEvent.Del(entity);

		//ApplyUpgradeUiModule
		public static bool HasApplyUpgradeUiModule(this ProtoEntity entity) =>
			s_GameAspect.ApplyUpgradeUiModule.Has(entity);

		public static ref ApplyUpgradeUiModuleComponent GetApplyUpgradeUiModule(this ProtoEntity entity) =>
			ref s_GameAspect.ApplyUpgradeUiModule.Get(entity);

		public static void ReplaceApplyUpgradeUiModule(this ProtoEntity entity, ApplyUpgradeModule value)
		{
			ref ApplyUpgradeUiModuleComponent applyUpgradeUiModuleComponent = ref s_GameAspect.ApplyUpgradeUiModule.Get(entity);
			applyUpgradeUiModuleComponent.Value = value;
		}

		public static ref ApplyUpgradeUiModuleComponent AddApplyUpgradeUiModule(this ProtoEntity entity, ApplyUpgradeModule value)
		{
			ref ApplyUpgradeUiModuleComponent applyUpgradeUiModuleComponent = ref s_GameAspect.ApplyUpgradeUiModule.Add(entity);
			applyUpgradeUiModuleComponent.Value = value;
			return ref applyUpgradeUiModuleComponent;
		}

		public static void DelApplyUpgradeUiModule(this ProtoEntity entity)
			=> s_GameAspect.ApplyUpgradeUiModule.Del(entity);

		//UpgradeConfig
		public static bool HasUpgradeConfig(this ProtoEntity entity) =>
			s_GameAspect.UpgradeConfig.Has(entity);

		public static ref UpgradeConfigComponent GetUpgradeConfig(this ProtoEntity entity) =>
			ref s_GameAspect.UpgradeConfig.Get(entity);

		public static void ReplaceUpgradeConfig(this ProtoEntity entity, UpgradeConfig value, int index)
		{
			ref UpgradeConfigComponent upgradeConfigComponent = ref s_GameAspect.UpgradeConfig.Get(entity);
			upgradeConfigComponent.Value = value;
			upgradeConfigComponent.Index = index;
		}

		public static ref UpgradeConfigComponent AddUpgradeConfig(this ProtoEntity entity, UpgradeConfig value, int index)
		{
			ref UpgradeConfigComponent upgradeConfigComponent = ref s_GameAspect.UpgradeConfig.Add(entity);
			upgradeConfigComponent.Value = value;
			upgradeConfigComponent.Index = index;
			return ref upgradeConfigComponent;
		}

		public static void DelUpgradeConfig(this ProtoEntity entity)
			=> s_GameAspect.UpgradeConfig.Del(entity);

		//UpgradeLink
		public static bool HasUpgradeLink(this ProtoEntity entity) =>
			s_GameAspect.UpgradeLink.Has(entity);

		public static ref UpgradeLinkComponent GetUpgradeLink(this ProtoEntity entity) =>
			ref s_GameAspect.UpgradeLink.Get(entity);

		public static void ReplaceUpgradeLink(this ProtoEntity entity, ProtoEntity value)
		{
			ref UpgradeLinkComponent upgradeLinkComponent = ref s_GameAspect.UpgradeLink.Get(entity);
			upgradeLinkComponent.Value = value;
		}

		public static ref UpgradeLinkComponent AddUpgradeLink(this ProtoEntity entity, ProtoEntity value)
		{
			ref UpgradeLinkComponent upgradeLinkComponent = ref s_GameAspect.UpgradeLink.Add(entity);
			upgradeLinkComponent.Value = value;
			return ref upgradeLinkComponent;
		}

		public static void DelUpgradeLink(this ProtoEntity entity)
			=> s_GameAspect.UpgradeLink.Del(entity);

		//Upgrade
		public static bool HasUpgrade(this ProtoEntity entity) =>
			s_GameAspect.Upgrade.Has(entity);

		public static ref UpgradeTag AddUpgrade(this ProtoEntity entity)
		{
			ref UpgradeTag upgradeTag = ref s_GameAspect.Upgrade.Add(entity);
			return ref upgradeTag;
		}

		public static void DelUpgrade(this ProtoEntity entity)
			=> s_GameAspect.Upgrade.Del(entity);

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

		//SwingingTree
		public static bool HasSwingingTree(this ProtoEntity entity) =>
			s_GameAspect.SwingingTree.Has(entity);

		public static ref SwingingTreeComponent GetSwingingTree(this ProtoEntity entity) =>
			ref s_GameAspect.SwingingTree.Get(entity);

		public static void ReplaceSwingingTree(this ProtoEntity entity, TreeType treeType, bool enableYAxisSwingingTree, float speedX, float speedY, float maxAngleX, float maxAngleY, float direction)
		{
			ref SwingingTreeComponent swingingTreeComponent = ref s_GameAspect.SwingingTree.Get(entity);
			swingingTreeComponent.TreeType = treeType;
			swingingTreeComponent.EnableYAxisSwingingTree = enableYAxisSwingingTree;
			swingingTreeComponent.SpeedX = speedX;
			swingingTreeComponent.SpeedY = speedY;
			swingingTreeComponent.MaxAngleX = maxAngleX;
			swingingTreeComponent.MaxAngleY = maxAngleY;
			swingingTreeComponent.Direction = direction;
		}

		public static ref SwingingTreeComponent AddSwingingTree(this ProtoEntity entity, TreeType treeType, bool enableYAxisSwingingTree, float speedX, float speedY, float maxAngleX, float maxAngleY, float direction)
		{
			ref SwingingTreeComponent swingingTreeComponent = ref s_GameAspect.SwingingTree.Add(entity);
			swingingTreeComponent.TreeType = treeType;
			swingingTreeComponent.EnableYAxisSwingingTree = enableYAxisSwingingTree;
			swingingTreeComponent.SpeedX = speedX;
			swingingTreeComponent.SpeedY = speedY;
			swingingTreeComponent.MaxAngleX = maxAngleX;
			swingingTreeComponent.MaxAngleY = maxAngleY;
			swingingTreeComponent.Direction = direction;
			return ref swingingTreeComponent;
		}

		public static void DelSwingingTree(this ProtoEntity entity)
			=> s_GameAspect.SwingingTree.Del(entity);

		//Tree
		public static bool HasTree(this ProtoEntity entity) =>
			s_GameAspect.Tree.Has(entity);

		public static ref TreeTag AddTree(this ProtoEntity entity)
		{
			ref TreeTag treeTag = ref s_GameAspect.Tree.Add(entity);
			return ref treeTag;
		}

		public static void DelTree(this ProtoEntity entity)
			=> s_GameAspect.Tree.Del(entity);

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

		//NukeAbility
		public static bool HasNukeAbility(this ProtoEntity entity) =>
			s_GameAspect.NukeAbility.Has(entity);

		public static ref NukeAbilityTag AddNukeAbility(this ProtoEntity entity)
		{
			ref NukeAbilityTag nukeAbilityTag = ref s_GameAspect.NukeAbility.Add(entity);
			return ref nukeAbilityTag;
		}

		public static void DelNukeAbility(this ProtoEntity entity)
			=> s_GameAspect.NukeAbility.Del(entity);

		//NukeBombLink
		public static bool HasNukeBombLink(this ProtoEntity entity) =>
			s_GameAspect.NukeBombLink.Has(entity);

		public static ref NukeBombLinkComponent GetNukeBombLink(this ProtoEntity entity) =>
			ref s_GameAspect.NukeBombLink.Get(entity);

		public static void ReplaceNukeBombLink(this ProtoEntity entity, ProtoEntity value)
		{
			ref NukeBombLinkComponent nukeBombLinkComponent = ref s_GameAspect.NukeBombLink.Get(entity);
			nukeBombLinkComponent.Value = value;
		}

		public static ref NukeBombLinkComponent AddNukeBombLink(this ProtoEntity entity, ProtoEntity value)
		{
			ref NukeBombLinkComponent nukeBombLinkComponent = ref s_GameAspect.NukeBombLink.Add(entity);
			nukeBombLinkComponent.Value = value;
			return ref nukeBombLinkComponent;
		}

		public static void DelNukeBombLink(this ProtoEntity entity)
			=> s_GameAspect.NukeBombLink.Del(entity);

		//NukeBomb
		public static bool HasNukeBomb(this ProtoEntity entity) =>
			s_GameAspect.NukeBomb.Has(entity);

		public static ref NukeBombTag AddNukeBomb(this ProtoEntity entity)
		{
			ref NukeBombTag nukeBombTag = ref s_GameAspect.NukeBomb.Add(entity);
			return ref nukeBombTag;
		}

		public static void DelNukeBomb(this ProtoEntity entity)
			=> s_GameAspect.NukeBomb.Del(entity);

		//NukeDamageCollider
		public static bool HasNukeDamageCollider(this ProtoEntity entity) =>
			s_GameAspect.NukeDamageCollider.Has(entity);

		public static ref NukeDamageColliderComponent GetNukeDamageCollider(this ProtoEntity entity) =>
			ref s_GameAspect.NukeDamageCollider.Get(entity);

		public static void ReplaceNukeDamageCollider(this ProtoEntity entity, BoxCollider value)
		{
			ref NukeDamageColliderComponent nukeDamageColliderComponent = ref s_GameAspect.NukeDamageCollider.Get(entity);
			nukeDamageColliderComponent.Value = value;
		}

		public static ref NukeDamageColliderComponent AddNukeDamageCollider(this ProtoEntity entity, BoxCollider value)
		{
			ref NukeDamageColliderComponent nukeDamageColliderComponent = ref s_GameAspect.NukeDamageCollider.Add(entity);
			nukeDamageColliderComponent.Value = value;
			return ref nukeDamageColliderComponent;
		}

		public static void DelNukeDamageCollider(this ProtoEntity entity)
			=> s_GameAspect.NukeDamageCollider.Del(entity);

		//NukeParticle
		public static bool HasNukeParticle(this ProtoEntity entity) =>
			s_GameAspect.NukeParticle.Has(entity);

		public static ref NukeParticleComponent GetNukeParticle(this ProtoEntity entity) =>
			ref s_GameAspect.NukeParticle.Get(entity);

		public static void ReplaceNukeParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref NukeParticleComponent nukeParticleComponent = ref s_GameAspect.NukeParticle.Get(entity);
			nukeParticleComponent.Value = value;
		}

		public static ref NukeParticleComponent AddNukeParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref NukeParticleComponent nukeParticleComponent = ref s_GameAspect.NukeParticle.Add(entity);
			nukeParticleComponent.Value = value;
			return ref nukeParticleComponent;
		}

		public static void DelNukeParticle(this ProtoEntity entity)
			=> s_GameAspect.NukeParticle.Del(entity);

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

		//HealthBuster
		public static bool HasHealthBuster(this ProtoEntity entity) =>
			s_GameAspect.HealthBuster.Has(entity);

		public static ref HealthBusterComponent GetHealthBuster(this ProtoEntity entity) =>
			ref s_GameAspect.HealthBuster.Get(entity);

		public static void ReplaceHealthBuster(this ProtoEntity entity, int value)
		{
			ref HealthBusterComponent healthBusterComponent = ref s_GameAspect.HealthBuster.Get(entity);
			healthBusterComponent.Value = value;
		}

		public static ref HealthBusterComponent AddHealthBuster(this ProtoEntity entity, int value)
		{
			ref HealthBusterComponent healthBusterComponent = ref s_GameAspect.HealthBuster.Add(entity);
			healthBusterComponent.Value = value;
			return ref healthBusterComponent;
		}

		public static void DelHealthBuster(this ProtoEntity entity)
			=> s_GameAspect.HealthBuster.Del(entity);

		//HealthBusterModule
		public static bool HasHealthBusterModule(this ProtoEntity entity) =>
			s_GameAspect.HealthBusterModule.Has(entity);

		public static ref HealthBusterModuleComponent GetHealthBusterModule(this ProtoEntity entity) =>
			ref s_GameAspect.HealthBusterModule.Get(entity);

		public static void ReplaceHealthBusterModule(this ProtoEntity entity, HealthBusterModule value)
		{
			ref HealthBusterModuleComponent healthBusterModuleComponent = ref s_GameAspect.HealthBusterModule.Get(entity);
			healthBusterModuleComponent.Value = value;
		}

		public static ref HealthBusterModuleComponent AddHealthBusterModule(this ProtoEntity entity, HealthBusterModule value)
		{
			ref HealthBusterModuleComponent healthBusterModuleComponent = ref s_GameAspect.HealthBusterModule.Add(entity);
			healthBusterModuleComponent.Value = value;
			return ref healthBusterModuleComponent;
		}

		public static void DelHealthBusterModule(this ProtoEntity entity)
			=> s_GameAspect.HealthBusterModule.Del(entity);

		//IncreaseHealthBoosterEvent
		public static bool HasIncreaseHealthBoosterEvent(this ProtoEntity entity) =>
			s_GameAspect.IncreaseHealthBoosterEvent.Has(entity);

		public static ref IncreaseHealthBoosterEvent GetIncreaseHealthBoosterEvent(this ProtoEntity entity) =>
			ref s_GameAspect.IncreaseHealthBoosterEvent.Get(entity);

		public static void ReplaceIncreaseHealthBoosterEvent(this ProtoEntity entity, int value)
		{
			ref IncreaseHealthBoosterEvent increaseHealthBoosterEvent = ref s_GameAspect.IncreaseHealthBoosterEvent.Get(entity);
			increaseHealthBoosterEvent.Value = value;
		}

		public static ref IncreaseHealthBoosterEvent AddIncreaseHealthBoosterEvent(this ProtoEntity entity, int value)
		{
			ref IncreaseHealthBoosterEvent increaseHealthBoosterEvent = ref s_GameAspect.IncreaseHealthBoosterEvent.Add(entity);
			increaseHealthBoosterEvent.Value = value;
			return ref increaseHealthBoosterEvent;
		}

		public static void DelIncreaseHealthBoosterEvent(this ProtoEntity entity)
			=> s_GameAspect.IncreaseHealthBoosterEvent.Del(entity);

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

		//FlameParticle
		public static bool HasFlameParticle(this ProtoEntity entity) =>
			s_GameAspect.FlameParticle.Has(entity);

		public static ref FlameParticleComponent GetFlameParticle(this ProtoEntity entity) =>
			ref s_GameAspect.FlameParticle.Get(entity);

		public static void ReplaceFlameParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref FlameParticleComponent flameParticleComponent = ref s_GameAspect.FlameParticle.Get(entity);
			flameParticleComponent.Value = value;
		}

		public static ref FlameParticleComponent AddFlameParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref FlameParticleComponent flameParticleComponent = ref s_GameAspect.FlameParticle.Add(entity);
			flameParticleComponent.Value = value;
			return ref flameParticleComponent;
		}

		public static void DelFlameParticle(this ProtoEntity entity)
			=> s_GameAspect.FlameParticle.Del(entity);

		//FlamethrowerAbility
		public static bool HasFlamethrowerAbility(this ProtoEntity entity) =>
			s_GameAspect.FlamethrowerAbility.Has(entity);

		public static ref FlamethrowerAbilityTag AddFlamethrowerAbility(this ProtoEntity entity)
		{
			ref FlamethrowerAbilityTag flamethrowerAbilityTag = ref s_GameAspect.FlamethrowerAbility.Add(entity);
			return ref flamethrowerAbilityTag;
		}

		public static void DelFlamethrowerAbility(this ProtoEntity entity)
			=> s_GameAspect.FlamethrowerAbility.Del(entity);

		//FlamethrowerLink
		public static bool HasFlamethrowerLink(this ProtoEntity entity) =>
			s_GameAspect.FlamethrowerLink.Has(entity);

		public static ref FlamethrowerLinkComponent GetFlamethrowerLink(this ProtoEntity entity) =>
			ref s_GameAspect.FlamethrowerLink.Get(entity);

		public static void ReplaceFlamethrowerLink(this ProtoEntity entity, ProtoEntity value)
		{
			ref FlamethrowerLinkComponent flamethrowerLinkComponent = ref s_GameAspect.FlamethrowerLink.Get(entity);
			flamethrowerLinkComponent.Value = value;
		}

		public static ref FlamethrowerLinkComponent AddFlamethrowerLink(this ProtoEntity entity, ProtoEntity value)
		{
			ref FlamethrowerLinkComponent flamethrowerLinkComponent = ref s_GameAspect.FlamethrowerLink.Add(entity);
			flamethrowerLinkComponent.Value = value;
			return ref flamethrowerLinkComponent;
		}

		public static void DelFlamethrowerLink(this ProtoEntity entity)
			=> s_GameAspect.FlamethrowerLink.Del(entity);

		//Flamethrower
		public static bool HasFlamethrower(this ProtoEntity entity) =>
			s_GameAspect.Flamethrower.Has(entity);

		public static ref FlamethrowerTag AddFlamethrower(this ProtoEntity entity)
		{
			ref FlamethrowerTag flamethrowerTag = ref s_GameAspect.Flamethrower.Add(entity);
			return ref flamethrowerTag;
		}

		public static void DelFlamethrower(this ProtoEntity entity)
			=> s_GameAspect.Flamethrower.Del(entity);

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

		//EnemySpawnerConfig
		public static bool HasEnemySpawnerConfig(this ProtoEntity entity) =>
			s_GameAspect.EnemySpawnerConfig.Has(entity);

		public static ref EnemySpawnerConfigComponent GetEnemySpawnerConfig(this ProtoEntity entity) =>
			ref s_GameAspect.EnemySpawnerConfig.Get(entity);

		public static void ReplaceEnemySpawnerConfig(this ProtoEntity entity, EnemySpawnerConfig value)
		{
			ref EnemySpawnerConfigComponent enemySpawnerConfigComponent = ref s_GameAspect.EnemySpawnerConfig.Get(entity);
			enemySpawnerConfigComponent.Value = value;
		}

		public static ref EnemySpawnerConfigComponent AddEnemySpawnerConfig(this ProtoEntity entity, EnemySpawnerConfig value)
		{
			ref EnemySpawnerConfigComponent enemySpawnerConfigComponent = ref s_GameAspect.EnemySpawnerConfig.Add(entity);
			enemySpawnerConfigComponent.Value = value;
			return ref enemySpawnerConfigComponent;
		}

		public static void DelEnemySpawnerConfig(this ProtoEntity entity)
			=> s_GameAspect.EnemySpawnerConfig.Del(entity);

		//EnemySpawnerData
		public static bool HasEnemySpawnerData(this ProtoEntity entity) =>
			s_GameAspect.EnemySpawnerData.Has(entity);

		public static ref EnemySpawnerDataComponent GetEnemySpawnerData(this ProtoEntity entity) =>
			ref s_GameAspect.EnemySpawnerData.Get(entity);

		public static void ReplaceEnemySpawnerData(this ProtoEntity entity, int spawnedEnemies, int spawnedBosses, int spawnedKamikaze, int waweIndex, EnemyType lastSpawnedEnemyType)
		{
			ref EnemySpawnerDataComponent enemySpawnerDataComponent = ref s_GameAspect.EnemySpawnerData.Get(entity);
			enemySpawnerDataComponent.SpawnedEnemies = spawnedEnemies;
			enemySpawnerDataComponent.SpawnedBosses = spawnedBosses;
			enemySpawnerDataComponent.SpawnedKamikaze = spawnedKamikaze;
			enemySpawnerDataComponent.WaweIndex = waweIndex;
			enemySpawnerDataComponent.LastSpawnedEnemyType = lastSpawnedEnemyType;
		}

		public static ref EnemySpawnerDataComponent AddEnemySpawnerData(this ProtoEntity entity, int spawnedEnemies, int spawnedBosses, int spawnedKamikaze, int waweIndex, EnemyType lastSpawnedEnemyType)
		{
			ref EnemySpawnerDataComponent enemySpawnerDataComponent = ref s_GameAspect.EnemySpawnerData.Add(entity);
			enemySpawnerDataComponent.SpawnedEnemies = spawnedEnemies;
			enemySpawnerDataComponent.SpawnedBosses = spawnedBosses;
			enemySpawnerDataComponent.SpawnedKamikaze = spawnedKamikaze;
			enemySpawnerDataComponent.WaweIndex = waweIndex;
			enemySpawnerDataComponent.LastSpawnedEnemyType = lastSpawnedEnemyType;
			return ref enemySpawnerDataComponent;
		}

		public static void DelEnemySpawnerData(this ProtoEntity entity)
			=> s_GameAspect.EnemySpawnerData.Del(entity);

		//EnemySpawner
		public static bool HasEnemySpawner(this ProtoEntity entity) =>
			s_GameAspect.EnemySpawner.Has(entity);

		public static ref EnemySpawnerTag AddEnemySpawner(this ProtoEntity entity)
		{
			ref EnemySpawnerTag enemySpawnerTag = ref s_GameAspect.EnemySpawner.Add(entity);
			return ref enemySpawnerTag;
		}

		public static void DelEnemySpawner(this ProtoEntity entity)
			=> s_GameAspect.EnemySpawner.Del(entity);

		//EnemySpawnerUiModule
		public static bool HasEnemySpawnerUiModule(this ProtoEntity entity) =>
			s_GameAspect.EnemySpawnerUiModule.Has(entity);

		public static ref EnemySpawnerUiModuleComponent GetEnemySpawnerUiModule(this ProtoEntity entity) =>
			ref s_GameAspect.EnemySpawnerUiModule.Get(entity);

		public static void ReplaceEnemySpawnerUiModule(this ProtoEntity entity, EnemySpawnerUiModule value)
		{
			ref EnemySpawnerUiModuleComponent enemySpawnerUiModuleComponent = ref s_GameAspect.EnemySpawnerUiModule.Get(entity);
			enemySpawnerUiModuleComponent.Value = value;
		}

		public static ref EnemySpawnerUiModuleComponent AddEnemySpawnerUiModule(this ProtoEntity entity, EnemySpawnerUiModule value)
		{
			ref EnemySpawnerUiModuleComponent enemySpawnerUiModuleComponent = ref s_GameAspect.EnemySpawnerUiModule.Add(entity);
			enemySpawnerUiModuleComponent.Value = value;
			return ref enemySpawnerUiModuleComponent;
		}

		public static void DelEnemySpawnerUiModule(this ProtoEntity entity)
			=> s_GameAspect.EnemySpawnerUiModule.Del(entity);

		//EnemySpawnPoints
		public static bool HasEnemySpawnPoints(this ProtoEntity entity) =>
			s_GameAspect.EnemySpawnPoints.Has(entity);

		public static ref EnemySpawnPointsComponent GetEnemySpawnPoints(this ProtoEntity entity) =>
			ref s_GameAspect.EnemySpawnPoints.Get(entity);

		public static void ReplaceEnemySpawnPoints(this ProtoEntity entity, ProtoEntity[,] value)
		{
			ref EnemySpawnPointsComponent enemySpawnPointsComponent = ref s_GameAspect.EnemySpawnPoints.Get(entity);
			enemySpawnPointsComponent.Value = value;
		}

		public static ref EnemySpawnPointsComponent AddEnemySpawnPoints(this ProtoEntity entity, ProtoEntity[,] value)
		{
			ref EnemySpawnPointsComponent enemySpawnPointsComponent = ref s_GameAspect.EnemySpawnPoints.Add(entity);
			enemySpawnPointsComponent.Value = value;
			return ref enemySpawnPointsComponent;
		}

		public static void DelEnemySpawnPoints(this ProtoEntity entity)
			=> s_GameAspect.EnemySpawnPoints.Del(entity);

		//EnemySpawnPoint
		public static bool HasEnemySpawnPoint(this ProtoEntity entity) =>
			s_GameAspect.EnemySpawnPoint.Has(entity);

		public static ref EnemySpawnPointTag AddEnemySpawnPoint(this ProtoEntity entity)
		{
			ref EnemySpawnPointTag enemySpawnPointTag = ref s_GameAspect.EnemySpawnPoint.Add(entity);
			return ref enemySpawnPointTag;
		}

		public static void DelEnemySpawnPoint(this ProtoEntity entity)
			=> s_GameAspect.EnemySpawnPoint.Del(entity);

		//TargetCharactersPoints
		public static bool HasTargetCharactersPoints(this ProtoEntity entity) =>
			s_GameAspect.TargetCharactersPoints.Has(entity);

		public static ref TargetCharactersPoints GetTargetCharactersPoints(this ProtoEntity entity) =>
			ref s_GameAspect.TargetCharactersPoints.Get(entity);

		public static void ReplaceTargetCharactersPoints(this ProtoEntity entity, ProtoEntity melee, ProtoEntity range)
		{
			ref TargetCharactersPoints targetCharactersPoints = ref s_GameAspect.TargetCharactersPoints.Get(entity);
			targetCharactersPoints.Melee = melee;
			targetCharactersPoints.Range = range;
		}

		public static ref TargetCharactersPoints AddTargetCharactersPoints(this ProtoEntity entity, ProtoEntity melee, ProtoEntity range)
		{
			ref TargetCharactersPoints targetCharactersPoints = ref s_GameAspect.TargetCharactersPoints.Add(entity);
			targetCharactersPoints.Melee = melee;
			targetCharactersPoints.Range = range;
			return ref targetCharactersPoints;
		}

		public static void DelTargetCharactersPoints(this ProtoEntity entity)
			=> s_GameAspect.TargetCharactersPoints.Del(entity);

		//WaveCompletedEvent
		public static bool HasWaveCompletedEvent(this ProtoEntity entity) =>
			s_GameAspect.WaveCompletedEvent.Has(entity);

		public static ref WaveCompletedEvent AddWaveCompletedEvent(this ProtoEntity entity)
		{
			ref WaveCompletedEvent waveCompletedEvent = ref s_GameAspect.WaveCompletedEvent.Add(entity);
			return ref waveCompletedEvent;
		}

		public static void DelWaveCompletedEvent(this ProtoEntity entity)
			=> s_GameAspect.WaveCompletedEvent.Del(entity);

		//CharacterMeleePoint
		public static bool HasCharacterMeleePoint(this ProtoEntity entity) =>
			s_GameAspect.CharacterMeleePoint.Has(entity);

		public static ref CharacterMeleePointComponent GetCharacterMeleePoint(this ProtoEntity entity) =>
			ref s_GameAspect.CharacterMeleePoint.Get(entity);

		public static void ReplaceCharacterMeleePoint(this ProtoEntity entity, ProtoEntity value)
		{
			ref CharacterMeleePointComponent characterMeleePointComponent = ref s_GameAspect.CharacterMeleePoint.Get(entity);
			characterMeleePointComponent.Value = value;
		}

		public static ref CharacterMeleePointComponent AddCharacterMeleePoint(this ProtoEntity entity, ProtoEntity value)
		{
			ref CharacterMeleePointComponent characterMeleePointComponent = ref s_GameAspect.CharacterMeleePoint.Add(entity);
			characterMeleePointComponent.Value = value;
			return ref characterMeleePointComponent;
		}

		public static void DelCharacterMeleePoint(this ProtoEntity entity)
			=> s_GameAspect.CharacterMeleePoint.Del(entity);

		//EnemyBoss
		public static bool HasEnemyBoss(this ProtoEntity entity) =>
			s_GameAspect.EnemyBoss.Has(entity);

		public static ref EnemyBossTag AddEnemyBoss(this ProtoEntity entity)
		{
			ref EnemyBossTag enemyBossTag = ref s_GameAspect.EnemyBoss.Add(entity);
			return ref enemyBossTag;
		}

		public static void DelEnemyBoss(this ProtoEntity entity)
			=> s_GameAspect.EnemyBoss.Del(entity);

		//EnemyKamikaze
		public static bool HasEnemyKamikaze(this ProtoEntity entity) =>
			s_GameAspect.EnemyKamikaze.Has(entity);

		public static ref EnemyKamikazeTag AddEnemyKamikaze(this ProtoEntity entity)
		{
			ref EnemyKamikazeTag enemyKamikazeTag = ref s_GameAspect.EnemyKamikaze.Add(entity);
			return ref enemyKamikazeTag;
		}

		public static void DelEnemyKamikaze(this ProtoEntity entity)
			=> s_GameAspect.EnemyKamikaze.Del(entity);

		//Enemy
		public static bool HasEnemy(this ProtoEntity entity) =>
			s_GameAspect.Enemy.Has(entity);

		public static ref EnemyTag AddEnemy(this ProtoEntity entity)
		{
			ref EnemyTag enemyTag = ref s_GameAspect.Enemy.Add(entity);
			return ref enemyTag;
		}

		public static void DelEnemy(this ProtoEntity entity)
			=> s_GameAspect.Enemy.Del(entity);

		//EnemyType
		public static bool HasEnemyType(this ProtoEntity entity) =>
			s_GameAspect.EnemyType.Has(entity);

		public static ref EnemyTypeComponent GetEnemyType(this ProtoEntity entity) =>
			ref s_GameAspect.EnemyType.Get(entity);

		public static void ReplaceEnemyType(this ProtoEntity entity, EnemyType value)
		{
			ref EnemyTypeComponent enemyTypeComponent = ref s_GameAspect.EnemyType.Get(entity);
			enemyTypeComponent.Value = value;
		}

		public static ref EnemyTypeComponent AddEnemyType(this ProtoEntity entity, EnemyType value)
		{
			ref EnemyTypeComponent enemyTypeComponent = ref s_GameAspect.EnemyType.Add(entity);
			enemyTypeComponent.Value = value;
			return ref enemyTypeComponent;
		}

		public static void DelEnemyType(this ProtoEntity entity)
			=> s_GameAspect.EnemyType.Del(entity);

		//Explode
		public static bool HasExplode(this ProtoEntity entity) =>
			s_GameAspect.Explode.Has(entity);

		public static ref ExplodeComponent AddExplode(this ProtoEntity entity)
		{
			ref ExplodeComponent explodeComponent = ref s_GameAspect.Explode.Add(entity);
			return ref explodeComponent;
		}

		public static void DelExplode(this ProtoEntity entity)
			=> s_GameAspect.Explode.Del(entity);

		//FindCharacters
		public static bool HasFindCharacters(this ProtoEntity entity) =>
			s_GameAspect.FindCharacters.Has(entity);

		public static ref FindCharactersComponent AddFindCharacters(this ProtoEntity entity)
		{
			ref FindCharactersComponent findCharactersComponent = ref s_GameAspect.FindCharacters.Add(entity);
			return ref findCharactersComponent;
		}

		public static void DelFindCharacters(this ProtoEntity entity)
			=> s_GameAspect.FindCharacters.Del(entity);

		//MassAttackParticle
		public static bool HasMassAttackParticle(this ProtoEntity entity) =>
			s_GameAspect.MassAttackParticle.Has(entity);

		public static ref MassAttackParticleComponent GetMassAttackParticle(this ProtoEntity entity) =>
			ref s_GameAspect.MassAttackParticle.Get(entity);

		public static void ReplaceMassAttackParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref MassAttackParticleComponent massAttackParticleComponent = ref s_GameAspect.MassAttackParticle.Get(entity);
			massAttackParticleComponent.Value = value;
		}

		public static ref MassAttackParticleComponent AddMassAttackParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref MassAttackParticleComponent massAttackParticleComponent = ref s_GameAspect.MassAttackParticle.Add(entity);
			massAttackParticleComponent.Value = value;
			return ref massAttackParticleComponent;
		}

		public static void DelMassAttackParticle(this ProtoEntity entity)
			=> s_GameAspect.MassAttackParticle.Del(entity);

		//MassAttackPower
		public static bool HasMassAttackPower(this ProtoEntity entity) =>
			s_GameAspect.MassAttackPower.Has(entity);

		public static ref MassAttackPowerComponent GetMassAttackPower(this ProtoEntity entity) =>
			ref s_GameAspect.MassAttackPower.Get(entity);

		public static void ReplaceMassAttackPower(this ProtoEntity entity, int value)
		{
			ref MassAttackPowerComponent massAttackPowerComponent = ref s_GameAspect.MassAttackPower.Get(entity);
			massAttackPowerComponent.Value = value;
		}

		public static ref MassAttackPowerComponent AddMassAttackPower(this ProtoEntity entity, int value)
		{
			ref MassAttackPowerComponent massAttackPowerComponent = ref s_GameAspect.MassAttackPower.Add(entity);
			massAttackPowerComponent.Value = value;
			return ref massAttackPowerComponent;
		}

		public static void DelMassAttackPower(this ProtoEntity entity)
			=> s_GameAspect.MassAttackPower.Del(entity);

		//MassAttackTimer
		public static bool HasMassAttackTimer(this ProtoEntity entity) =>
			s_GameAspect.MassAttackTimer.Has(entity);

		public static ref MassAttackTimerComponent GetMassAttackTimer(this ProtoEntity entity) =>
			ref s_GameAspect.MassAttackTimer.Get(entity);

		public static void ReplaceMassAttackTimer(this ProtoEntity entity, float value)
		{
			ref MassAttackTimerComponent massAttackTimerComponent = ref s_GameAspect.MassAttackTimer.Get(entity);
			massAttackTimerComponent.Value = value;
		}

		public static ref MassAttackTimerComponent AddMassAttackTimer(this ProtoEntity entity, float value)
		{
			ref MassAttackTimerComponent massAttackTimerComponent = ref s_GameAspect.MassAttackTimer.Add(entity);
			massAttackTimerComponent.Value = value;
			return ref massAttackTimerComponent;
		}

		public static void DelMassAttackTimer(this ProtoEntity entity)
			=> s_GameAspect.MassAttackTimer.Del(entity);

		//ReachedMeleePoint
		public static bool HasReachedMeleePoint(this ProtoEntity entity) =>
			s_GameAspect.ReachedMeleePoint.Has(entity);

		public static ref ReachedMeleePointComponent AddReachedMeleePoint(this ProtoEntity entity)
		{
			ref ReachedMeleePointComponent reachedMeleePointComponent = ref s_GameAspect.ReachedMeleePoint.Add(entity);
			return ref reachedMeleePointComponent;
		}

		public static void DelReachedMeleePoint(this ProtoEntity entity)
			=> s_GameAspect.ReachedMeleePoint.Del(entity);

		//TargetBunker
		public static bool HasTargetBunker(this ProtoEntity entity) =>
			s_GameAspect.TargetBunker.Has(entity);

		public static ref TargetBunkerComponent GetTargetBunker(this ProtoEntity entity) =>
			ref s_GameAspect.TargetBunker.Get(entity);

		public static void ReplaceTargetBunker(this ProtoEntity entity, ProtoEntity value)
		{
			ref TargetBunkerComponent targetBunkerComponent = ref s_GameAspect.TargetBunker.Get(entity);
			targetBunkerComponent.Value = value;
		}

		public static ref TargetBunkerComponent AddTargetBunker(this ProtoEntity entity, ProtoEntity value)
		{
			ref TargetBunkerComponent targetBunkerComponent = ref s_GameAspect.TargetBunker.Add(entity);
			targetBunkerComponent.Value = value;
			return ref targetBunkerComponent;
		}

		public static void DelTargetBunker(this ProtoEntity entity)
			=> s_GameAspect.TargetBunker.Del(entity);

		//TargetCharacter
		public static bool HasTargetCharacter(this ProtoEntity entity) =>
			s_GameAspect.TargetCharacter.Has(entity);

		public static ref TargetCharacterComponent GetTargetCharacter(this ProtoEntity entity) =>
			ref s_GameAspect.TargetCharacter.Get(entity);

		public static void ReplaceTargetCharacter(this ProtoEntity entity, ProtoEntity value)
		{
			ref TargetCharacterComponent targetCharacterComponent = ref s_GameAspect.TargetCharacter.Get(entity);
			targetCharacterComponent.Value = value;
		}

		public static ref TargetCharacterComponent AddTargetCharacter(this ProtoEntity entity, ProtoEntity value)
		{
			ref TargetCharacterComponent targetCharacterComponent = ref s_GameAspect.TargetCharacter.Add(entity);
			targetCharacterComponent.Value = value;
			return ref targetCharacterComponent;
		}

		public static void DelTargetCharacter(this ProtoEntity entity)
			=> s_GameAspect.TargetCharacter.Del(entity);

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

		//CharacterSpawner
		public static bool HasCharacterSpawner(this ProtoEntity entity) =>
			s_GameAspect.CharacterSpawner.Has(entity);

		public static ref CharacterSpawnerTag AddCharacterSpawner(this ProtoEntity entity)
		{
			ref CharacterSpawnerTag characterSpawnerTag = ref s_GameAspect.CharacterSpawner.Add(entity);
			return ref characterSpawnerTag;
		}

		public static void DelCharacterSpawner(this ProtoEntity entity)
			=> s_GameAspect.CharacterSpawner.Del(entity);

		//CharacterSpawnPoint
		public static bool HasCharacterSpawnPoint(this ProtoEntity entity) =>
			s_GameAspect.CharacterSpawnPoint.Has(entity);

		public static ref CharacterSpawnPointTag AddCharacterSpawnPoint(this ProtoEntity entity)
		{
			ref CharacterSpawnPointTag characterSpawnPointTag = ref s_GameAspect.CharacterSpawnPoint.Add(entity);
			return ref characterSpawnPointTag;
		}

		public static void DelCharacterSpawnPoint(this ProtoEntity entity)
			=> s_GameAspect.CharacterSpawnPoint.Del(entity);

		//CharacterSpawnPointType
		public static bool HasCharacterSpawnPointType(this ProtoEntity entity) =>
			s_GameAspect.CharacterSpawnPointType.Has(entity);

		public static ref CharacterSpawnPointTypeComponent GetCharacterSpawnPointType(this ProtoEntity entity) =>
			ref s_GameAspect.CharacterSpawnPointType.Get(entity);

		public static void ReplaceCharacterSpawnPointType(this ProtoEntity entity, SpawnPointType value)
		{
			ref CharacterSpawnPointTypeComponent characterSpawnPointTypeComponent = ref s_GameAspect.CharacterSpawnPointType.Get(entity);
			characterSpawnPointTypeComponent.Value = value;
		}

		public static ref CharacterSpawnPointTypeComponent AddCharacterSpawnPointType(this ProtoEntity entity, SpawnPointType value)
		{
			ref CharacterSpawnPointTypeComponent characterSpawnPointTypeComponent = ref s_GameAspect.CharacterSpawnPointType.Add(entity);
			characterSpawnPointTypeComponent.Value = value;
			return ref characterSpawnPointTypeComponent;
		}

		public static void DelCharacterSpawnPointType(this ProtoEntity entity)
			=> s_GameAspect.CharacterSpawnPointType.Del(entity);

		//CharactersSpawnPoints
		public static bool HasCharactersSpawnPoints(this ProtoEntity entity) =>
			s_GameAspect.CharactersSpawnPoints.Has(entity);

		public static ref CharactersSpawnPointsComponent GetCharactersSpawnPoints(this ProtoEntity entity) =>
			ref s_GameAspect.CharactersSpawnPoints.Get(entity);

		public static void ReplaceCharactersSpawnPoints(this ProtoEntity entity, List<ProtoEntity> meleeSpawnPoints, List<ProtoEntity> rangeSpawnPoints)
		{
			ref CharactersSpawnPointsComponent charactersSpawnPointsComponent = ref s_GameAspect.CharactersSpawnPoints.Get(entity);
			charactersSpawnPointsComponent.MeleeSpawnPoints = meleeSpawnPoints;
			charactersSpawnPointsComponent.RangeSpawnPoints = rangeSpawnPoints;
		}

		public static ref CharactersSpawnPointsComponent AddCharactersSpawnPoints(this ProtoEntity entity, List<ProtoEntity> meleeSpawnPoints, List<ProtoEntity> rangeSpawnPoints)
		{
			ref CharactersSpawnPointsComponent charactersSpawnPointsComponent = ref s_GameAspect.CharactersSpawnPoints.Add(entity);
			charactersSpawnPointsComponent.MeleeSpawnPoints = meleeSpawnPoints;
			charactersSpawnPointsComponent.RangeSpawnPoints = rangeSpawnPoints;
			return ref charactersSpawnPointsComponent;
		}

		public static void DelCharactersSpawnPoints(this ProtoEntity entity)
			=> s_GameAspect.CharactersSpawnPoints.Del(entity);

		//SpawnCharactersEvent
		public static bool HasSpawnCharactersEvent(this ProtoEntity entity) =>
			s_GameAspect.SpawnCharactersEvent.Has(entity);

		public static ref SpawnCharactersEvent AddSpawnCharactersEvent(this ProtoEntity entity)
		{
			ref SpawnCharactersEvent spawnCharactersEvent = ref s_GameAspect.SpawnCharactersEvent.Add(entity);
			return ref spawnCharactersEvent;
		}

		public static void DelSpawnCharactersEvent(this ProtoEntity entity)
			=> s_GameAspect.SpawnCharactersEvent.Del(entity);

		//CharacterMeleeConfig
		public static bool HasCharacterMeleeConfig(this ProtoEntity entity) =>
			s_GameAspect.CharacterMeleeConfig.Has(entity);

		public static ref CharacterMeleeConfigComponent GetCharacterMeleeConfig(this ProtoEntity entity) =>
			ref s_GameAspect.CharacterMeleeConfig.Get(entity);

		public static void ReplaceCharacterMeleeConfig(this ProtoEntity entity, CharacterMeleeConfig value)
		{
			ref CharacterMeleeConfigComponent characterMeleeConfigComponent = ref s_GameAspect.CharacterMeleeConfig.Get(entity);
			characterMeleeConfigComponent.Value = value;
		}

		public static ref CharacterMeleeConfigComponent AddCharacterMeleeConfig(this ProtoEntity entity, CharacterMeleeConfig value)
		{
			ref CharacterMeleeConfigComponent characterMeleeConfigComponent = ref s_GameAspect.CharacterMeleeConfig.Add(entity);
			characterMeleeConfigComponent.Value = value;
			return ref characterMeleeConfigComponent;
		}

		public static void DelCharacterMeleeConfig(this ProtoEntity entity)
			=> s_GameAspect.CharacterMeleeConfig.Del(entity);

		//CharacterMelee
		public static bool HasCharacterMelee(this ProtoEntity entity) =>
			s_GameAspect.CharacterMelee.Has(entity);

		public static ref CharacterMeleeTag AddCharacterMelee(this ProtoEntity entity)
		{
			ref CharacterMeleeTag characterMeleeTag = ref s_GameAspect.CharacterMelee.Add(entity);
			return ref characterMeleeTag;
		}

		public static void DelCharacterMelee(this ProtoEntity entity)
			=> s_GameAspect.CharacterMelee.Del(entity);

		//CharacterRange
		public static bool HasCharacterRange(this ProtoEntity entity) =>
			s_GameAspect.CharacterRange.Has(entity);

		public static ref CharacterRangeTag AddCharacterRange(this ProtoEntity entity)
		{
			ref CharacterRangeTag characterRangeTag = ref s_GameAspect.CharacterRange.Add(entity);
			return ref characterRangeTag;
		}

		public static void DelCharacterRange(this ProtoEntity entity)
			=> s_GameAspect.CharacterRange.Del(entity);

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

		//CharacterType
		public static bool HasCharacterType(this ProtoEntity entity) =>
			s_GameAspect.CharacterType.Has(entity);

		public static ref CharacterTypeComponent GetCharacterType(this ProtoEntity entity) =>
			ref s_GameAspect.CharacterType.Get(entity);

		public static void ReplaceCharacterType(this ProtoEntity entity, CharacterType value)
		{
			ref CharacterTypeComponent characterTypeComponent = ref s_GameAspect.CharacterType.Get(entity);
			characterTypeComponent.Value = value;
		}

		public static ref CharacterTypeComponent AddCharacterType(this ProtoEntity entity, CharacterType value)
		{
			ref CharacterTypeComponent characterTypeComponent = ref s_GameAspect.CharacterType.Add(entity);
			characterTypeComponent.Value = value;
			return ref characterTypeComponent;
		}

		public static void DelCharacterType(this ProtoEntity entity)
			=> s_GameAspect.CharacterType.Del(entity);

		//EnemiesFindRange
		public static bool HasEnemiesFindRange(this ProtoEntity entity) =>
			s_GameAspect.EnemiesFindRange.Has(entity);

		public static ref EnemiesFindRangeComponent GetEnemiesFindRange(this ProtoEntity entity) =>
			ref s_GameAspect.EnemiesFindRange.Get(entity);

		public static void ReplaceEnemiesFindRange(this ProtoEntity entity, float value)
		{
			ref EnemiesFindRangeComponent enemiesFindRangeComponent = ref s_GameAspect.EnemiesFindRange.Get(entity);
			enemiesFindRangeComponent.Value = value;
		}

		public static ref EnemiesFindRangeComponent AddEnemiesFindRange(this ProtoEntity entity, float value)
		{
			ref EnemiesFindRangeComponent enemiesFindRangeComponent = ref s_GameAspect.EnemiesFindRange.Add(entity);
			enemiesFindRangeComponent.Value = value;
			return ref enemiesFindRangeComponent;
		}

		public static void DelEnemiesFindRange(this ProtoEntity entity)
			=> s_GameAspect.EnemiesFindRange.Del(entity);

		//FindEnemies
		public static bool HasFindEnemies(this ProtoEntity entity) =>
			s_GameAspect.FindEnemies.Has(entity);

		public static ref FindEnemiesComponent AddFindEnemies(this ProtoEntity entity)
		{
			ref FindEnemiesComponent findEnemiesComponent = ref s_GameAspect.FindEnemies.Add(entity);
			return ref findEnemiesComponent;
		}

		public static void DelFindEnemies(this ProtoEntity entity)
			=> s_GameAspect.FindEnemies.Del(entity);

		//MassAttackEvent
		public static bool HasMassAttackEvent(this ProtoEntity entity) =>
			s_GameAspect.MassAttackEvent.Has(entity);

		public static ref MassAttackEvent AddMassAttackEvent(this ProtoEntity entity)
		{
			ref MassAttackEvent massAttackEvent = ref s_GameAspect.MassAttackEvent.Add(entity);
			return ref massAttackEvent;
		}

		public static void DelMassAttackEvent(this ProtoEntity entity)
			=> s_GameAspect.MassAttackEvent.Del(entity);

		//TargetEnemy
		public static bool HasTargetEnemy(this ProtoEntity entity) =>
			s_GameAspect.TargetEnemy.Has(entity);

		public static ref TargetEnemyComponent GetTargetEnemy(this ProtoEntity entity) =>
			ref s_GameAspect.TargetEnemy.Get(entity);

		public static void ReplaceTargetEnemy(this ProtoEntity entity, ProtoEntity value)
		{
			ref TargetEnemyComponent targetEnemyComponent = ref s_GameAspect.TargetEnemy.Get(entity);
			targetEnemyComponent.Value = value;
		}

		public static ref TargetEnemyComponent AddTargetEnemy(this ProtoEntity entity, ProtoEntity value)
		{
			ref TargetEnemyComponent targetEnemyComponent = ref s_GameAspect.TargetEnemy.Add(entity);
			targetEnemyComponent.Value = value;
			return ref targetEnemyComponent;
		}

		public static void DelTargetEnemy(this ProtoEntity entity)
			=> s_GameAspect.TargetEnemy.Del(entity);

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

		//BurnEvent
		public static bool HasBurnEvent(this ProtoEntity entity) =>
			s_GameAspect.BurnEvent.Has(entity);

		public static ref BurnEvent AddBurnEvent(this ProtoEntity entity)
		{
			ref BurnEvent burnEvent = ref s_GameAspect.BurnEvent.Add(entity);
			return ref burnEvent;
		}

		public static void DelBurnEvent(this ProtoEntity entity)
			=> s_GameAspect.BurnEvent.Del(entity);

		//BurnParticle
		public static bool HasBurnParticle(this ProtoEntity entity) =>
			s_GameAspect.BurnParticle.Has(entity);

		public static ref BurnParticleComponent GetBurnParticle(this ProtoEntity entity) =>
			ref s_GameAspect.BurnParticle.Get(entity);

		public static void ReplaceBurnParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref BurnParticleComponent burnParticleComponent = ref s_GameAspect.BurnParticle.Get(entity);
			burnParticleComponent.Value = value;
		}

		public static ref BurnParticleComponent AddBurnParticle(this ProtoEntity entity, ParticleSystem value)
		{
			ref BurnParticleComponent burnParticleComponent = ref s_GameAspect.BurnParticle.Add(entity);
			burnParticleComponent.Value = value;
			return ref burnParticleComponent;
		}

		public static void DelBurnParticle(this ProtoEntity entity)
			=> s_GameAspect.BurnParticle.Del(entity);

		//BurnPeriodicTimer
		public static bool HasBurnPeriodicTimer(this ProtoEntity entity) =>
			s_GameAspect.BurnPeriodicTimer.Has(entity);

		public static ref BurnPeriodicTimerComponent GetBurnPeriodicTimer(this ProtoEntity entity) =>
			ref s_GameAspect.BurnPeriodicTimer.Get(entity);

		public static void ReplaceBurnPeriodicTimer(this ProtoEntity entity, float value)
		{
			ref BurnPeriodicTimerComponent burnPeriodicTimerComponent = ref s_GameAspect.BurnPeriodicTimer.Get(entity);
			burnPeriodicTimerComponent.Value = value;
		}

		public static ref BurnPeriodicTimerComponent AddBurnPeriodicTimer(this ProtoEntity entity, float value)
		{
			ref BurnPeriodicTimerComponent burnPeriodicTimerComponent = ref s_GameAspect.BurnPeriodicTimer.Add(entity);
			burnPeriodicTimerComponent.Value = value;
			return ref burnPeriodicTimerComponent;
		}

		public static void DelBurnPeriodicTimer(this ProtoEntity entity)
			=> s_GameAspect.BurnPeriodicTimer.Del(entity);

		//BurnTimer
		public static bool HasBurnTimer(this ProtoEntity entity) =>
			s_GameAspect.BurnTimer.Has(entity);

		public static ref BurnTimerComponent GetBurnTimer(this ProtoEntity entity) =>
			ref s_GameAspect.BurnTimer.Get(entity);

		public static void ReplaceBurnTimer(this ProtoEntity entity, float value)
		{
			ref BurnTimerComponent burnTimerComponent = ref s_GameAspect.BurnTimer.Get(entity);
			burnTimerComponent.Value = value;
		}

		public static ref BurnTimerComponent AddBurnTimer(this ProtoEntity entity, float value)
		{
			ref BurnTimerComponent burnTimerComponent = ref s_GameAspect.BurnTimer.Add(entity);
			burnTimerComponent.Value = value;
			return ref burnTimerComponent;
		}

		public static void DelBurnTimer(this ProtoEntity entity)
			=> s_GameAspect.BurnTimer.Del(entity);

		//ForbiddingUseBurnTimer
		public static bool HasForbiddingUseBurnTimer(this ProtoEntity entity) =>
			s_GameAspect.ForbiddingUseBurnTimer.Has(entity);

		public static ref ForbiddingUseBurnTimerComponent GetForbiddingUseBurnTimer(this ProtoEntity entity) =>
			ref s_GameAspect.ForbiddingUseBurnTimer.Get(entity);

		public static void ReplaceForbiddingUseBurnTimer(this ProtoEntity entity, float value)
		{
			ref ForbiddingUseBurnTimerComponent forbiddingUseBurnTimerComponent = ref s_GameAspect.ForbiddingUseBurnTimer.Get(entity);
			forbiddingUseBurnTimerComponent.Value = value;
		}

		public static ref ForbiddingUseBurnTimerComponent AddForbiddingUseBurnTimer(this ProtoEntity entity, float value)
		{
			ref ForbiddingUseBurnTimerComponent forbiddingUseBurnTimerComponent = ref s_GameAspect.ForbiddingUseBurnTimer.Add(entity);
			forbiddingUseBurnTimerComponent.Value = value;
			return ref forbiddingUseBurnTimerComponent;
		}

		public static void DelForbiddingUseBurnTimer(this ProtoEntity entity)
			=> s_GameAspect.ForbiddingUseBurnTimer.Del(entity);

		//Bunker
		public static bool HasBunker(this ProtoEntity entity) =>
			s_GameAspect.Bunker.Has(entity);

		public static ref BunkerTag AddBunker(this ProtoEntity entity)
		{
			ref BunkerTag bunkerTag = ref s_GameAspect.Bunker.Add(entity);
			return ref bunkerTag;
		}

		public static void DelBunker(this ProtoEntity entity)
			=> s_GameAspect.Bunker.Del(entity);

		//BunkerUiModule
		public static bool HasBunkerUiModule(this ProtoEntity entity) =>
			s_GameAspect.BunkerUiModule.Has(entity);

		public static ref BunkerUiModuleComponent GetBunkerUiModule(this ProtoEntity entity) =>
			ref s_GameAspect.BunkerUiModule.Get(entity);

		public static void ReplaceBunkerUiModule(this ProtoEntity entity, BunkerUiModule value)
		{
			ref BunkerUiModuleComponent bunkerUiModuleComponent = ref s_GameAspect.BunkerUiModule.Get(entity);
			bunkerUiModuleComponent.Value = value;
		}

		public static ref BunkerUiModuleComponent AddBunkerUiModule(this ProtoEntity entity, BunkerUiModule value)
		{
			ref BunkerUiModuleComponent bunkerUiModuleComponent = ref s_GameAspect.BunkerUiModule.Add(entity);
			bunkerUiModuleComponent.Value = value;
			return ref bunkerUiModuleComponent;
		}

		public static void DelBunkerUiModule(this ProtoEntity entity)
			=> s_GameAspect.BunkerUiModule.Del(entity);

		//AbilityApplierModule
		public static bool HasAbilityApplierModule(this ProtoEntity entity) =>
			s_GameAspect.AbilityApplierModule.Has(entity);

		public static ref AbilityApplierModuleComponent GetAbilityApplierModule(this ProtoEntity entity) =>
			ref s_GameAspect.AbilityApplierModule.Get(entity);

		public static void ReplaceAbilityApplierModule(this ProtoEntity entity, AbilityApplierModule value)
		{
			ref AbilityApplierModuleComponent abilityApplierModuleComponent = ref s_GameAspect.AbilityApplierModule.Get(entity);
			abilityApplierModuleComponent.Value = value;
		}

		public static ref AbilityApplierModuleComponent AddAbilityApplierModule(this ProtoEntity entity, AbilityApplierModule value)
		{
			ref AbilityApplierModuleComponent abilityApplierModuleComponent = ref s_GameAspect.AbilityApplierModule.Add(entity);
			abilityApplierModuleComponent.Value = value;
			return ref abilityApplierModuleComponent;
		}

		public static void DelAbilityApplierModule(this ProtoEntity entity)
			=> s_GameAspect.AbilityApplierModule.Del(entity);

		//AbilityCooldownDuration
		public static bool HasAbilityCooldownDuration(this ProtoEntity entity) =>
			s_GameAspect.AbilityCooldownDuration.Has(entity);

		public static ref AbilityCooldownDurationComponent GetAbilityCooldownDuration(this ProtoEntity entity) =>
			ref s_GameAspect.AbilityCooldownDuration.Get(entity);

		public static void ReplaceAbilityCooldownDuration(this ProtoEntity entity, float value)
		{
			ref AbilityCooldownDurationComponent abilityCooldownDurationComponent = ref s_GameAspect.AbilityCooldownDuration.Get(entity);
			abilityCooldownDurationComponent.Value = value;
		}

		public static ref AbilityCooldownDurationComponent AddAbilityCooldownDuration(this ProtoEntity entity, float value)
		{
			ref AbilityCooldownDurationComponent abilityCooldownDurationComponent = ref s_GameAspect.AbilityCooldownDuration.Add(entity);
			abilityCooldownDurationComponent.Value = value;
			return ref abilityCooldownDurationComponent;
		}

		public static void DelAbilityCooldownDuration(this ProtoEntity entity)
			=> s_GameAspect.AbilityCooldownDuration.Del(entity);

		//Ability
		public static bool HasAbility(this ProtoEntity entity) =>
			s_GameAspect.Ability.Has(entity);

		public static ref AbilityTag AddAbility(this ProtoEntity entity)
		{
			ref AbilityTag abilityTag = ref s_GameAspect.Ability.Add(entity);
			return ref abilityTag;
		}

		public static void DelAbility(this ProtoEntity entity)
			=> s_GameAspect.Ability.Del(entity);

		//ApplyAbilityEvent
		public static bool HasApplyAbilityEvent(this ProtoEntity entity) =>
			s_GameAspect.ApplyAbilityEvent.Has(entity);

		public static ref ApplyAbilityEvent AddApplyAbilityEvent(this ProtoEntity entity)
		{
			ref ApplyAbilityEvent applyAbilityEvent = ref s_GameAspect.ApplyAbilityEvent.Add(entity);
			return ref applyAbilityEvent;
		}

		public static void DelApplyAbilityEvent(this ProtoEntity entity)
			=> s_GameAspect.ApplyAbilityEvent.Del(entity);

		//ChangeForDurationTime
		public static bool HasChangeForDurationTime(this ProtoEntity entity) =>
			s_GameAspect.ChangeForDurationTime.Has(entity);

		public static ref ChangeForDurationTimeComponent GetChangeForDurationTime(this ProtoEntity entity) =>
			ref s_GameAspect.ChangeForDurationTime.Get(entity);

		public static void ReplaceChangeForDurationTime(this ProtoEntity entity, float duration, float animationTime, float targetValue, float fromValue, float value)
		{
			ref ChangeForDurationTimeComponent changeForDurationTimeComponent = ref s_GameAspect.ChangeForDurationTime.Get(entity);
			changeForDurationTimeComponent.Duration = duration;
			changeForDurationTimeComponent.AnimationTime = animationTime;
			changeForDurationTimeComponent.TargetValue = targetValue;
			changeForDurationTimeComponent.FromValue = fromValue;
			changeForDurationTimeComponent.Value = value;
		}

		public static ref ChangeForDurationTimeComponent AddChangeForDurationTime(this ProtoEntity entity, float duration, float animationTime, float targetValue, float fromValue, float value)
		{
			ref ChangeForDurationTimeComponent changeForDurationTimeComponent = ref s_GameAspect.ChangeForDurationTime.Add(entity);
			changeForDurationTimeComponent.Duration = duration;
			changeForDurationTimeComponent.AnimationTime = animationTime;
			changeForDurationTimeComponent.TargetValue = targetValue;
			changeForDurationTimeComponent.FromValue = fromValue;
			changeForDurationTimeComponent.Value = value;
			return ref changeForDurationTimeComponent;
		}

		public static void DelChangeForDurationTime(this ProtoEntity entity)
			=> s_GameAspect.ChangeForDurationTime.Del(entity);

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

		//AchievementModule
		public static bool HasAchievementModule(this ProtoEntity entity) =>
			s_GameAspect.AchievementModule.Has(entity);

		public static ref AchievementModuleComponent GetAchievementModule(this ProtoEntity entity) =>
			ref s_GameAspect.AchievementModule.Get(entity);

		public static void ReplaceAchievementModule(this ProtoEntity entity, AchievementModule value)
		{
			ref AchievementModuleComponent achievementModuleComponent = ref s_GameAspect.AchievementModule.Get(entity);
			achievementModuleComponent.Value = value;
		}

		public static ref AchievementModuleComponent AddAchievementModule(this ProtoEntity entity, AchievementModule value)
		{
			ref AchievementModuleComponent achievementModuleComponent = ref s_GameAspect.AchievementModule.Add(entity);
			achievementModuleComponent.Value = value;
			return ref achievementModuleComponent;
		}

		public static void DelAchievementModule(this ProtoEntity entity)
			=> s_GameAspect.AchievementModule.Del(entity);

		//Achievement
		public static bool HasAchievement(this ProtoEntity entity) =>
			s_GameAspect.Achievement.Has(entity);

		public static ref AchievementTag AddAchievement(this ProtoEntity entity)
		{
			ref AchievementTag achievementTag = ref s_GameAspect.Achievement.Add(entity);
			return ref achievementTag;
		}

		public static void DelAchievement(this ProtoEntity entity)
			=> s_GameAspect.Achievement.Del(entity);

		//FirstUsedCompleted
		public static bool HasFirstUsedCompleted(this ProtoEntity entity) =>
			s_GameAspect.FirstUsedCompleted.Has(entity);

		public static ref FirstUsedCompletedComponent AddFirstUsedCompleted(this ProtoEntity entity)
		{
			ref FirstUsedCompletedComponent firstUsedCompletedComponent = ref s_GameAspect.FirstUsedCompleted.Add(entity);
			return ref firstUsedCompletedComponent;
		}

		public static void DelFirstUsedCompleted(this ProtoEntity entity)
			=> s_GameAspect.FirstUsedCompleted.Del(entity);

		//SelectAchievementEvent
		public static bool HasSelectAchievementEvent(this ProtoEntity entity) =>
			s_GameAspect.SelectAchievementEvent.Has(entity);

		public static ref SelectAchievementEvent AddSelectAchievementEvent(this ProtoEntity entity)
		{
			ref SelectAchievementEvent selectAchievementEvent = ref s_GameAspect.SelectAchievementEvent.Add(entity);
			return ref selectAchievementEvent;
		}

		public static void DelSelectAchievementEvent(this ProtoEntity entity)
			=> s_GameAspect.SelectAchievementEvent.Del(entity);

	}
}
