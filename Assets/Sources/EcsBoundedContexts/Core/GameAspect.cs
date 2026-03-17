using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation;
using Sources.Frameworks.GameServices.EntityPools.Domain.Components;
using Sources.EcsBoundedContexts.Weapons.Domain;
using Sources.EcsBoundedContexts.Volumes.Domain.Components;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.EcsBoundedContexts.Tutorials.Domain.Components;
using Sources.EcsBoundedContexts.Timers.Domain;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.Particles.Domain;
using Sources.EcsBoundedContexts.Movements.TargetPoint.Components;
using Sources.EcsBoundedContexts.Movements.Rotation.Components;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using Sources.EcsBoundedContexts.LookAt.Domain;
using Sources.EcsBoundedContexts.Lights.Domain;
using Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Components;
using Sources.EcsBoundedContexts.GraphOwners.Domain;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using Sources.EcsBoundedContexts.ExplosionBodies.Domain;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;
using Sources.EcsBoundedContexts.Damage.Domain;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Sources.EcsBoundedContexts.BurnAbilities.Domain.Components;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.Animators;
using Sources.EcsBoundedContexts.AnimatorLod.Domain.Components;
using Sources.EcsBoundedContexts.Animancers.Domain.Components;
using Sources.EcsBoundedContexts.Achievements.Domain.Components;

namespace Sources.EcsBoundedContexts.Core
{
	public class GameAspect : ProtoAspectInject
	{
		public readonly Dictionary<Type, IProtoPool> Pools;

		//Default

		//Common
		public readonly ProtoPool<ReturnToPoolActionComponent> ReturnToPoolAction = new ();
		public readonly ProtoPool<GunOwnerModuleComponent> GunOwnerModule = new ();
		public readonly ProtoPool<ChangeVolumeEvent> ChangeVolumeEvent = new ();
		public readonly ProtoPool<GameVolumeComponent> GameVolume = new ();
		public readonly ProtoPool<MutedVolumeComponent> MutedVolume = new ();
		public readonly ProtoPool<MuteVolumeEvent> MuteVolumeEvent = new ();
		public readonly ProtoPool<UnmuteVolumeEvent> UnmuteVolumeEvent = new ();
		public readonly ProtoPool<VolumeModuleComponent> VolumeModule = new ();
		public readonly ProtoPool<VolumeTypeComponent> VolumeType = new ();
		public readonly ProtoPool<TimerComponent> Timer = new ();
		public readonly ProtoPool<ClearableDataComponent> ClearableData = new ();
		public readonly ProtoPool<ClearDataEvent> ClearDataEvent = new ();
		public readonly ProtoPool<SavableDataComponent> SavableData = new ();
		public readonly ProtoPool<SaveDataEvent> SaveDataEvent = new ();
		public readonly ProtoPool<PointPathComponent> PointPath = new ();
		public readonly ProtoPool<TargetPointComponent> TargetPoint = new ();
		public readonly ProtoPool<TargetPointIndexComponent> TargetPointIndex = new ();
		public readonly ProtoPool<RotationSpeedComponent> RotationSpeed = new ();
		public readonly ProtoPool<TurnSideComponent> TurnSide = new ();
		public readonly ProtoPool<ChangeSpeedDeltaComponent> ChangeSpeedDelta = new ();
		public readonly ProtoPool<CompleteMoveAlongPathEvent> CompleteMoveAlongPathEvent = new ();
		public readonly ProtoPool<CompleteMoveAlongPathPointEvent> CompleteMoveAlongPathPointEvent = new ();
		public readonly ProtoPool<MoveSpeedComponent> MoveSpeed = new ();
		public readonly ProtoPool<NavMeshComponent> NavMesh = new ();
		public readonly ProtoPool<TargetSpeedComponent> TargetSpeed = new ();
		public readonly ProtoPool<LookAtComponent> LookAt = new ();
		public readonly ProtoPool<KillEnemyCounterComponent> KillEnemyCounter = new ();
		public readonly ProtoPool<ActiveComponent> Active = new ();
		public readonly ProtoPool<DisableGameObjectEvent> DisableGameObjectEvent = new ();
		public readonly ProtoPool<EnableGameObjectEvent> EnableGameObjectEvent = new ();
		public readonly ProtoPool<GameObjectComponent> GameObject = new ();
		public readonly ProtoPool<BloodParticleComponent> BloodParticle = new ();
		public readonly ProtoPool<DamageEvent> DamageEvent = new ();
		public readonly ProtoPool<HealthTextComponent> HealthText = new ();
		public readonly ProtoPool<ApplyDailyRewardEvent> ApplyDailyRewardEvent = new ();
		public readonly ProtoPool<DailyRewardDataComponent> DailyRewardData = new ();
		public readonly ProtoPool<DailyRewardModuleComponent> DailyRewardModule = new ();
		public readonly ProtoPool<DailyRewardTag> DailyReward = new ();
		public readonly ProtoPool<CompleteComponent> Complete = new ();
		public readonly ProtoPool<DecreaseEvent> DecreaseEvent = new ();
		public readonly ProtoPool<DistanceComponent> Distance = new ();
		public readonly ProtoPool<EntityLinkComponent> EntityLink = new ();
		public readonly ProtoPool<HeadTransformComponent> HeadTransform = new ();
		public readonly ProtoPool<IncreaseEvent> IncreaseEvent = new ();
		public readonly ProtoPool<InitializedComponent> Initialized = new ();
		public readonly ProtoPool<InitializeEvent> InitializeEvent = new ();
		public readonly ProtoPool<InPoolComponent> InPool = new ();
		public readonly ProtoPool<ScaleComponent> Scale = new ();
		public readonly ProtoPool<SequenceComponent> Sequence = new ();
		public readonly ProtoPool<StringIdComponent> StringId = new ();
		public readonly ProtoPool<TransformComponent> Transform = new ();
		public readonly ProtoPool<AnimatorComponent> Animator = new ();
		public readonly ProtoPool<AnimancerEcsComponent> AnimancerEcs = new ();
		public readonly ProtoPool<AnimancerStateComponent> AnimancerState = new ();

		//EventBuffer
		public readonly ProtoPool<EventBufferTag> EventBuffer = new ();

		//Player
		public readonly ProtoPool<CoinsChangedEvent> CoinsChangedEvent = new ();
		public readonly ProtoPool<DecreaseCoinsEvent> DecreaseCoinsEvent = new ();
		public readonly ProtoPool<IncreaseCoinsEvent> IncreaseCoinsEvent = new ();
		public readonly ProtoPool<PlayerWalletComponent> PlayerWallet = new ();
		public readonly ProtoPool<PlayerWalletModuleComponent> PlayerWalletModule = new ();
		public readonly ProtoPool<ChangeRotationSpeedDeltaComponent> ChangeRotationSpeedDelta = new ();
		public readonly ProtoPool<TargetRotationComponent> TargetRotation = new ();
		public readonly ProtoPool<TargetRotationSpeedComponent> TargetRotationSpeed = new ();
		public readonly ProtoPool<CharacterControllerComponent> CharacterController = new ();
		public readonly ProtoPool<MoveDeltaComponent> MoveDelta = new ();
		public readonly ProtoPool<BehaviourTreeOwnerComponent> BehaviourTreeOwner = new ();
		public readonly ProtoPool<FsmOwnerComponent> FsmOwner = new ();
		public readonly ProtoPool<DamageTimerComponent> DamageTimer = new ();
		public readonly ProtoPool<DeadEvent> DeadEvent = new ();
		public readonly ProtoPool<HealthBarComponent> HealthBar = new ();
		public readonly ProtoPool<HealthComponent> Health = new ();
		public readonly ProtoPool<MaxHealthComponent> MaxHealth = new ();
		public readonly ProtoPool<ObstacleCollisionEvent> ObstacleCollisionEvent = new ();

		//Tree
		public readonly ProtoPool<TutorialTag> Tutorial = new ();

		//Camera
		public readonly ProtoPool<CameraComponent> Camera = new ();
		public readonly ProtoPool<CinemachineCameraComponent> CinemachineCamera = new ();
		public readonly ProtoPool<MainCameraTag> MainCamera = new ();

		//AnimatorLod
		public readonly ProtoPool<AnimatorLodComponent> AnimatorLod = new ();

		//Light
		public readonly ProtoPool<FireLightComponent> FireLight = new ();
		public readonly ProtoPool<FlickeringLightComponent> FlickeringLight = new ();
		public readonly ProtoPool<LightComponent> Light = new ();
		public readonly ProtoPool<PeriodicLightComponent> PeriodicLight = new ();
		public readonly ProtoPool<ShadowControllerComponent> ShadowController = new ();

		//Chunks

		//Ability
		public readonly ProtoPool<ExplosionBodyBloodyTag> ExplosionBodyBloody = new ();
		public readonly ProtoPool<ExplosionBodyTag> ExplosionBody = new ();
		public readonly ProtoPool<AvailableComponent> Available = new ();
		public readonly ProtoPool<BurnEvent> BurnEvent = new ();
		public readonly ProtoPool<BurnPeriodicTimerComponent> BurnPeriodicTimer = new ();
		public readonly ProtoPool<BurnTimerComponent> BurnTimer = new ();
		public readonly ProtoPool<ForbiddingUseBurnTimerComponent> ForbiddingUseBurnTimer = new ();
		public readonly ProtoPool<AbilityApplierModuleComponent> AbilityApplierModule = new ();
		public readonly ProtoPool<AbilityCooldownDurationComponent> AbilityCooldownDuration = new ();
		public readonly ProtoPool<AbilityTag> Ability = new ();
		public readonly ProtoPool<ApplyAbilityEvent> ApplyAbilityEvent = new ();
		public readonly ProtoPool<ChangeForDurationTimeComponent> ChangeForDurationTime = new ();

		//Characters
		public readonly ProtoPool<HealParticleComponent> HealParticle = new ();
		public readonly ProtoPool<ShootParticleComponent> ShootParticle = new ();
		public readonly ProtoPool<AttackPowerComponent> AttackPower = new ();
		public readonly ProtoPool<CharacterMeleeConfigComponent> CharacterMeleeConfig = new ();
		public readonly ProtoPool<CharacterMeleeTag> CharacterMelee = new ();
		public readonly ProtoPool<CharacterRangeTag> CharacterRange = new ();
		public readonly ProtoPool<CharacterTag> Character = new ();
		public readonly ProtoPool<CharacterTypeComponent> CharacterType = new ();
		public readonly ProtoPool<EnemiesFindRangeComponent> EnemiesFindRange = new ();
		public readonly ProtoPool<FindEnemiesComponent> FindEnemies = new ();
		public readonly ProtoPool<MassAttackEvent> MassAttackEvent = new ();
		public readonly ProtoPool<TargetEnemyComponent> TargetEnemy = new ();

		//Enemy
		public readonly ProtoPool<CharacterMeleePointComponent> CharacterMeleePoint = new ();
		public readonly ProtoPool<EnemyBossTag> EnemyBoss = new ();
		public readonly ProtoPool<EnemyKamikazeTag> EnemyKamikaze = new ();
		public readonly ProtoPool<EnemyTag> Enemy = new ();
		public readonly ProtoPool<EnemyTypeComponent> EnemyType = new ();
		public readonly ProtoPool<ExplodeComponent> Explode = new ();
		public readonly ProtoPool<FindCharactersComponent> FindCharacters = new ();
		public readonly ProtoPool<MassAttackParticleComponent> MassAttackParticle = new ();
		public readonly ProtoPool<MassAttackPowerComponent> MassAttackPower = new ();
		public readonly ProtoPool<MassAttackTimerComponent> MassAttackTimer = new ();
		public readonly ProtoPool<ReachedMeleePointComponent> ReachedMeleePoint = new ();
		public readonly ProtoPool<TargetBunkerComponent> TargetBunker = new ();
		public readonly ProtoPool<TargetCharacterComponent> TargetCharacter = new ();
		public readonly ProtoPool<BurnParticleComponent> BurnParticle = new ();

		//Upgrade
		public readonly ProtoPool<ApplyUpgradeEvent> ApplyUpgradeEvent = new ();
		public readonly ProtoPool<ApplyUpgradeUiModuleComponent> ApplyUpgradeUiModule = new ();
		public readonly ProtoPool<UpgradeConfigComponent> UpgradeConfig = new ();
		public readonly ProtoPool<UpgradeLinkComponent> UpgradeLink = new ();
		public readonly ProtoPool<UpgradeTag> Upgrade = new ();

		//Achievements
		public readonly ProtoPool<AchievementModuleComponent> AchievementModule = new ();
		public readonly ProtoPool<AchievementTag> Achievement = new ();
		public readonly ProtoPool<FirstUsedCompletedComponent> FirstUsedCompleted = new ();
		public readonly ProtoPool<SelectAchievementEvent> SelectAchievementEvent = new ();

		//Tutorial

		public GameAspect()
		{
			Pools = new ()
			{
				[typeof(ProtoPool<EventBufferTag>)] = EventBuffer,
				[typeof(ProtoPool<ReturnToPoolActionComponent>)] = ReturnToPoolAction,
				[typeof(ProtoPool<GunOwnerModuleComponent>)] = GunOwnerModule,
				[typeof(ProtoPool<ChangeVolumeEvent>)] = ChangeVolumeEvent,
				[typeof(ProtoPool<GameVolumeComponent>)] = GameVolume,
				[typeof(ProtoPool<MutedVolumeComponent>)] = MutedVolume,
				[typeof(ProtoPool<MuteVolumeEvent>)] = MuteVolumeEvent,
				[typeof(ProtoPool<UnmuteVolumeEvent>)] = UnmuteVolumeEvent,
				[typeof(ProtoPool<VolumeModuleComponent>)] = VolumeModule,
				[typeof(ProtoPool<VolumeTypeComponent>)] = VolumeType,
				[typeof(ProtoPool<ApplyUpgradeEvent>)] = ApplyUpgradeEvent,
				[typeof(ProtoPool<ApplyUpgradeUiModuleComponent>)] = ApplyUpgradeUiModule,
				[typeof(ProtoPool<UpgradeConfigComponent>)] = UpgradeConfig,
				[typeof(ProtoPool<UpgradeLinkComponent>)] = UpgradeLink,
				[typeof(ProtoPool<UpgradeTag>)] = Upgrade,
				[typeof(ProtoPool<TutorialTag>)] = Tutorial,
				[typeof(ProtoPool<TimerComponent>)] = Timer,
				[typeof(ProtoPool<ClearableDataComponent>)] = ClearableData,
				[typeof(ProtoPool<ClearDataEvent>)] = ClearDataEvent,
				[typeof(ProtoPool<SavableDataComponent>)] = SavableData,
				[typeof(ProtoPool<SaveDataEvent>)] = SaveDataEvent,
				[typeof(ProtoPool<CoinsChangedEvent>)] = CoinsChangedEvent,
				[typeof(ProtoPool<DecreaseCoinsEvent>)] = DecreaseCoinsEvent,
				[typeof(ProtoPool<IncreaseCoinsEvent>)] = IncreaseCoinsEvent,
				[typeof(ProtoPool<PlayerWalletComponent>)] = PlayerWallet,
				[typeof(ProtoPool<PlayerWalletModuleComponent>)] = PlayerWalletModule,
				[typeof(ProtoPool<HealParticleComponent>)] = HealParticle,
				[typeof(ProtoPool<ShootParticleComponent>)] = ShootParticle,
				[typeof(ProtoPool<PointPathComponent>)] = PointPath,
				[typeof(ProtoPool<TargetPointComponent>)] = TargetPoint,
				[typeof(ProtoPool<TargetPointIndexComponent>)] = TargetPointIndex,
				[typeof(ProtoPool<ChangeRotationSpeedDeltaComponent>)] = ChangeRotationSpeedDelta,
				[typeof(ProtoPool<RotationSpeedComponent>)] = RotationSpeed,
				[typeof(ProtoPool<TargetRotationComponent>)] = TargetRotation,
				[typeof(ProtoPool<TargetRotationSpeedComponent>)] = TargetRotationSpeed,
				[typeof(ProtoPool<TurnSideComponent>)] = TurnSide,
				[typeof(ProtoPool<ChangeSpeedDeltaComponent>)] = ChangeSpeedDelta,
				[typeof(ProtoPool<CharacterControllerComponent>)] = CharacterController,
				[typeof(ProtoPool<CompleteMoveAlongPathEvent>)] = CompleteMoveAlongPathEvent,
				[typeof(ProtoPool<CompleteMoveAlongPathPointEvent>)] = CompleteMoveAlongPathPointEvent,
				[typeof(ProtoPool<MoveDeltaComponent>)] = MoveDelta,
				[typeof(ProtoPool<MoveSpeedComponent>)] = MoveSpeed,
				[typeof(ProtoPool<NavMeshComponent>)] = NavMesh,
				[typeof(ProtoPool<TargetSpeedComponent>)] = TargetSpeed,
				[typeof(ProtoPool<LookAtComponent>)] = LookAt,
				[typeof(ProtoPool<FireLightComponent>)] = FireLight,
				[typeof(ProtoPool<FlickeringLightComponent>)] = FlickeringLight,
				[typeof(ProtoPool<LightComponent>)] = Light,
				[typeof(ProtoPool<PeriodicLightComponent>)] = PeriodicLight,
				[typeof(ProtoPool<ShadowControllerComponent>)] = ShadowController,
				[typeof(ProtoPool<KillEnemyCounterComponent>)] = KillEnemyCounter,
				[typeof(ProtoPool<BehaviourTreeOwnerComponent>)] = BehaviourTreeOwner,
				[typeof(ProtoPool<FsmOwnerComponent>)] = FsmOwner,
				[typeof(ProtoPool<ActiveComponent>)] = Active,
				[typeof(ProtoPool<DisableGameObjectEvent>)] = DisableGameObjectEvent,
				[typeof(ProtoPool<EnableGameObjectEvent>)] = EnableGameObjectEvent,
				[typeof(ProtoPool<GameObjectComponent>)] = GameObject,
				[typeof(ProtoPool<ExplosionBodyBloodyTag>)] = ExplosionBodyBloody,
				[typeof(ProtoPool<ExplosionBodyTag>)] = ExplosionBody,
				[typeof(ProtoPool<CharacterMeleePointComponent>)] = CharacterMeleePoint,
				[typeof(ProtoPool<EnemyBossTag>)] = EnemyBoss,
				[typeof(ProtoPool<EnemyKamikazeTag>)] = EnemyKamikaze,
				[typeof(ProtoPool<EnemyTag>)] = Enemy,
				[typeof(ProtoPool<EnemyTypeComponent>)] = EnemyType,
				[typeof(ProtoPool<ExplodeComponent>)] = Explode,
				[typeof(ProtoPool<FindCharactersComponent>)] = FindCharacters,
				[typeof(ProtoPool<MassAttackParticleComponent>)] = MassAttackParticle,
				[typeof(ProtoPool<MassAttackPowerComponent>)] = MassAttackPower,
				[typeof(ProtoPool<MassAttackTimerComponent>)] = MassAttackTimer,
				[typeof(ProtoPool<ReachedMeleePointComponent>)] = ReachedMeleePoint,
				[typeof(ProtoPool<TargetBunkerComponent>)] = TargetBunker,
				[typeof(ProtoPool<TargetCharacterComponent>)] = TargetCharacter,
				[typeof(ProtoPool<BloodParticleComponent>)] = BloodParticle,
				[typeof(ProtoPool<DamageEvent>)] = DamageEvent,
				[typeof(ProtoPool<DamageTimerComponent>)] = DamageTimer,
				[typeof(ProtoPool<DeadEvent>)] = DeadEvent,
				[typeof(ProtoPool<HealthBarComponent>)] = HealthBar,
				[typeof(ProtoPool<HealthComponent>)] = Health,
				[typeof(ProtoPool<HealthTextComponent>)] = HealthText,
				[typeof(ProtoPool<MaxHealthComponent>)] = MaxHealth,
				[typeof(ProtoPool<ObstacleCollisionEvent>)] = ObstacleCollisionEvent,
				[typeof(ProtoPool<ApplyDailyRewardEvent>)] = ApplyDailyRewardEvent,
				[typeof(ProtoPool<DailyRewardDataComponent>)] = DailyRewardData,
				[typeof(ProtoPool<DailyRewardModuleComponent>)] = DailyRewardModule,
				[typeof(ProtoPool<DailyRewardTag>)] = DailyReward,
				[typeof(ProtoPool<AttackPowerComponent>)] = AttackPower,
				[typeof(ProtoPool<AvailableComponent>)] = Available,
				[typeof(ProtoPool<CompleteComponent>)] = Complete,
				[typeof(ProtoPool<DecreaseEvent>)] = DecreaseEvent,
				[typeof(ProtoPool<DistanceComponent>)] = Distance,
				[typeof(ProtoPool<EntityLinkComponent>)] = EntityLink,
				[typeof(ProtoPool<HeadTransformComponent>)] = HeadTransform,
				[typeof(ProtoPool<IncreaseEvent>)] = IncreaseEvent,
				[typeof(ProtoPool<InitializedComponent>)] = Initialized,
				[typeof(ProtoPool<InitializeEvent>)] = InitializeEvent,
				[typeof(ProtoPool<InPoolComponent>)] = InPool,
				[typeof(ProtoPool<ScaleComponent>)] = Scale,
				[typeof(ProtoPool<SequenceComponent>)] = Sequence,
				[typeof(ProtoPool<StringIdComponent>)] = StringId,
				[typeof(ProtoPool<TransformComponent>)] = Transform,
				[typeof(ProtoPool<CharacterMeleeConfigComponent>)] = CharacterMeleeConfig,
				[typeof(ProtoPool<CharacterMeleeTag>)] = CharacterMelee,
				[typeof(ProtoPool<CharacterRangeTag>)] = CharacterRange,
				[typeof(ProtoPool<CharacterTag>)] = Character,
				[typeof(ProtoPool<CharacterTypeComponent>)] = CharacterType,
				[typeof(ProtoPool<EnemiesFindRangeComponent>)] = EnemiesFindRange,
				[typeof(ProtoPool<FindEnemiesComponent>)] = FindEnemies,
				[typeof(ProtoPool<MassAttackEvent>)] = MassAttackEvent,
				[typeof(ProtoPool<TargetEnemyComponent>)] = TargetEnemy,
				[typeof(ProtoPool<CameraComponent>)] = Camera,
				[typeof(ProtoPool<CinemachineCameraComponent>)] = CinemachineCamera,
				[typeof(ProtoPool<MainCameraTag>)] = MainCamera,
				[typeof(ProtoPool<BurnEvent>)] = BurnEvent,
				[typeof(ProtoPool<BurnParticleComponent>)] = BurnParticle,
				[typeof(ProtoPool<BurnPeriodicTimerComponent>)] = BurnPeriodicTimer,
				[typeof(ProtoPool<BurnTimerComponent>)] = BurnTimer,
				[typeof(ProtoPool<ForbiddingUseBurnTimerComponent>)] = ForbiddingUseBurnTimer,
				[typeof(ProtoPool<AbilityApplierModuleComponent>)] = AbilityApplierModule,
				[typeof(ProtoPool<AbilityCooldownDurationComponent>)] = AbilityCooldownDuration,
				[typeof(ProtoPool<AbilityTag>)] = Ability,
				[typeof(ProtoPool<ApplyAbilityEvent>)] = ApplyAbilityEvent,
				[typeof(ProtoPool<ChangeForDurationTimeComponent>)] = ChangeForDurationTime,
				[typeof(ProtoPool<AnimatorComponent>)] = Animator,
				[typeof(ProtoPool<AnimatorLodComponent>)] = AnimatorLod,
				[typeof(ProtoPool<AnimancerEcsComponent>)] = AnimancerEcs,
				[typeof(ProtoPool<AnimancerStateComponent>)] = AnimancerState,
				[typeof(ProtoPool<AchievementModuleComponent>)] = AchievementModule,
				[typeof(ProtoPool<AchievementTag>)] = Achievement,
				[typeof(ProtoPool<FirstUsedCompletedComponent>)] = FirstUsedCompleted,
				[typeof(ProtoPool<SelectAchievementEvent>)] = SelectAchievementEvent,
			};

			GameAspectExt.Construct(this);
		}
	}
}
