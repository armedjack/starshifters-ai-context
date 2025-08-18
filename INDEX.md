# AI Context Snapshot

Дата: 2025-08-18 22:06:02 UTC
Коммит: 01ee51d

## Последние 20 коммитов
- 01ee51d 2025-08-19 Eugeniy Gatsalov — Merge pull request #34 from armedjack/codex-rmavm6
- 3b3f875 2025-08-19 Eugeniy Gatsalov — fix: ensure drag events wired
- 811aab6 2025-08-18 Eugeniy Gatsalov — Merge pull request #33 from armedjack/codex-91kw93
- 56de3d6 2025-08-18 Eugeniy Gatsalov — fix: ensure drag events wired
- 2cc70eb 2025-08-18 Eugeniy Gatsalov — Merge pull request #32 from armedjack/codex/fix-card-drag-and-discard-issues-dy31eu
- 8e82938 2025-08-18 Eugeniy Gatsalov — Merge branch 'master' into codex/fix-card-drag-and-discard-issues-dy31eu
- ed26278 2025-08-18 Eugeniy Gatsalov — Expose CardGroup trigger handlers
- c55a7ff 2025-08-18 Eugeniy Gatsalov — Merge pull request #31 from armedjack/codex/fix-card-drag-and-discard-issues-7jirpt
- 9694df4 2025-08-18 Eugeniy Gatsalov — feat: support 3D triggers in relay
- 3bdfaa9 2025-08-18 Eugeniy Gatsalov — Merge pull request #30 from armedjack/codex/fix-card-drag-and-discard-issues
- 1f4874f 2025-08-18 Eugeniy Gatsalov — Fix card play and discard flow
- 50af0c7 2025-08-18 Evgeniy Gatsalov — extra font
- 3ff57ae 2025-08-17 Evgeniy Gatsalov — fix scene
- d60805d 2025-08-17 Eugeniy Gatsalov — Merge pull request #29 from armedjack/codex/fix-unassigned-flipanimator-reference
- 65e1588 2025-08-17 Eugeniy Gatsalov — Handle missing FlipAnimator on Card
- 0ed0119 2025-08-17 Eugeniy Gatsalov — Merge pull request #28 from armedjack/codex/fix-cs1513-error-in-cardhousegamebridge.cs
- 7197c6f 2025-08-17 Eugeniy Gatsalov — Fix missing closing brace in CardHouseGameBridge
- 0d33a72 2025-08-17 Eugeniy Gatsalov — Merge pull request #27 from armedjack/codex/fix-card-display-area-for-hand-0bsqc8
- eccb88c 2025-08-17 Eugeniy Gatsalov — Merge branch 'master' into codex/fix-card-display-area-for-hand-0bsqc8
- 15537b8 2025-08-17 Eugeniy Gatsalov — Rename local variable to avoid name conflict
## Дерево файлов (срез)
```
./Assets/CardHouse/CardHouseCore/CardBits/Poker/Scriptables/PokerCardDefinition.cs
./Assets/CardHouse/CardHouseCore/CardBits/Poker/Scripts/PokerCard.cs
./Assets/CardHouse/CardHouseCore/CardBits/Poker/Scripts/PokerSuit.cs
./Assets/CardHouse/CardHouseCore/CardBits/Tarot/Scripts/ArcanaData.cs
./Assets/CardHouse/CardHouseCore/CardBits/Tarot/Scripts/MajorArcanaName.cs
./Assets/CardHouse/CardHouseCore/CardBits/Tarot/Scripts/TarotCard.cs
./Assets/CardHouse/CardHouseCore/CardBits/Tarot/Scripts/TarotCardDefinition.cs
./Assets/CardHouse/CardHouseCore/CardBits/Tarot/Scripts/TarotSuit.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Card.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/CardFacing.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Operators/Activatable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Operators/CardTargetCardOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Operators/CardTransferOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Operators/DiscardCardOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Operators/DiscardTargetCardOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Operators/ShuffleOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Scriptables/CardDefinition.cs
./Assets/CardHouse/CardHouseCore/Scripts/Card/Scriptables/DeckDefinition.cs
./Assets/CardHouse/CardHouseCore/Scripts/Click/ClickDetector.cs
./Assets/CardHouse/CardHouseCore/Scripts/Click/PhaseGateClick.cs
./Assets/CardHouse/CardHouseCore/Scripts/ConditionalOperators/GroupConditional.cs
./Assets/CardHouse/CardHouseCore/Scripts/ConditionalOperators/PhaseConditional.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyChangeDetector.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyContainer.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyCost.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyQuantity.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyRegistry.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyUI.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/CurrencyWallet.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/Operators/CurrencyOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/Operators/CurrencyRefillOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Currencies/Operators/IncrementCurrencyOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Dragging/DragAction.cs
./Assets/CardHouse/CardHouseCore/Scripts/Dragging/DragDetector.cs
./Assets/CardHouse/CardHouseCore/Scripts/Dragging/DragOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Dragging/Dragging.cs
./Assets/CardHouse/CardHouseCore/Scripts/Events/EventChain.cs
./Assets/CardHouse/CardHouseCore/Scripts/Events/TimedEvent.cs
./Assets/CardHouse/CardHouseCore/Scripts/Gates/BlockAllDrops.cs
./Assets/CardHouse/CardHouseCore/Scripts/Gates/CurrencyGate.cs
./Assets/CardHouse/CardHouseCore/Scripts/Gates/Gate.cs
./Assets/CardHouse/CardHouseCore/Scripts/Gates/GateCollection.cs
./Assets/CardHouse/CardHouseCore/Scripts/Gates/Params/DropParams.cs
./Assets/CardHouse/CardHouseCore/Scripts/Gates/Params/NoParams.cs
./Assets/CardHouse/CardHouseCore/Scripts/Gates/Params/TargetCardParams.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/CardGroup.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/GroupInteractability.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/GroupName.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/GroupRegistry.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/Layouts/CardGridLayout.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/Layouts/CardGroupSettings.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/Layouts/SlotLayout.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/Layouts/SplayLayout.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/Layouts/StackLayout.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/MountDetector.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/MountingMode.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/TransitionData/DragTransition.cs
./Assets/CardHouse/CardHouseCore/Scripts/Groups/TransitionData/GroupTransition.cs
./Assets/CardHouse/CardHouseCore/Scripts/Hover/HoverDetector.cs
./Assets/CardHouse/CardHouseCore/Scripts/Hover/TriggerEnterRelay.cs
./Assets/CardHouse/CardHouseCore/Scripts/LifetimeDestructor.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/CardDropGateDimmer.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/CardLoyalty.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/DragGateDimmer.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/GroupDropGateDimmer.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/Loyalty.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/LoyaltyGateCardDrop.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/Phase.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/PhaseChangeDetector.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/PhaseGateCardDragStart.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/PhaseGateCardDrop.cs
./Assets/CardHouse/CardHouseCore/Scripts/Phases/PhaseManager.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Components/BaseSeekerComponent.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Components/Homing.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Components/Scaling.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Components/Turning.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/AnimCurveFloatSeeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/AnimCurveFloatSeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/ExponentialAngleFloatSeeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/ExponentialAngleFloatSeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/ExponentialFloatSeeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/ExponentialFloatSeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/InstantFloatSeeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/WaypointCurveFloatAngleSeeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/WaypointCurveFloatAngleSeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/WaypointCurveFloatSeeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Float/WaypointCurveFloatSeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Seeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/SeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/SeekerScriptableSet.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/SeekerSet.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/SeekerSetList.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/AnimCurveVector3Seeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/AnimCurveVector3SeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/ContinuousInstantVector3Seeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/ContinuousInstantVector3SeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/ExponentialVector3Seeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/ExponentialVector3SeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/InstantVector3Seeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/RandomizedCurveVector3SeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/TweakVector3Seeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/TweakVector3SeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/WaypointCurveVector3Seeker.cs
./Assets/CardHouse/CardHouseCore/Scripts/Seekers/Vector3/WaypointCurveVector3SeekerScriptable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Setup/CardSetup.cs
./Assets/CardHouse/CardHouseCore/Scripts/Setup/DeckSetup.cs
./Assets/CardHouse/CardHouseCore/Scripts/Setup/GroupSetup.cs
./Assets/CardHouse/CardHouseCore/Scripts/Setup/MultiplayerBoardSetup.cs
./Assets/CardHouse/CardHouseCore/Scripts/SpriteOperators/MultiSpriteOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/SpriteOperators/SpriteColorOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/SpriteOperators/SpriteImageOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/SpriteOperators/SpriteOperator.cs
./Assets/CardHouse/CardHouseCore/Scripts/Toggleable.cs
./Assets/CardHouse/CardHouseCore/Scripts/Utils.cs
./Assets/CardHouse/SampleGames/DeckBuilder/Scripts/DamageGroupOperator.cs
./Assets/CardHouse/SampleGames/DeckBuilder/Scripts/DamageTargetOperator.cs
./Assets/CardHouse/SampleGames/DeckBuilder/Scripts/Health.cs
./Assets/CardHouse/SampleGames/MemoryMatch/Scripts/MemoryCard.cs
./Assets/CardHouse/SampleGames/MemoryMatch/Scripts/MemoryGame.cs
./Assets/CardHouse/SampleGames/MemoryMatch/Scripts/MemoryUI.cs
./Assets/CardHouse/SampleGames/Solitaire/Scripts/SolitaireCardDragHandler.cs
./Assets/CardHouse/SampleGames/Solitaire/Scripts/SolitaireColumnChangeHandler.cs
./Assets/CardHouse/SampleGames/Solitaire/Scripts/SolitaireColumnDropGate.cs
./Assets/CardHouse/SampleGames/Solitaire/Scripts/SolitaireDeckClickHandler.cs
./Assets/CardHouse/SampleGames/Solitaire/Scripts/SolitaireScorePileDropGate.cs
./Assets/CardHouse/SampleGames/Solitaire/Scripts/SolitaireSetup.cs
./Assets/CardHouse/SampleGames/Tarot/Scripts/SpreadManager.cs
./Assets/CardHouse/SampleGames/Tarot/Scripts/TarotSpread.cs
./Assets/CardHouse/Tutorial/Editor/LaunchTutorialOption.cs
./Assets/CardHouse/Tutorial/LaunchDataScriptable.cs
./Assets/CardHouse/Tutorial/Lessons/1.1 - Card Drag/CardDragTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/2.2 - Group Setup/GroupSetupTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/2.6 - Mounting Mode/ClosestCardHighlighter.cs
./Assets/CardHouse/Tutorial/Lessons/2.7 - Stack Settings/StackTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/2.8 - Splay Settings/SplayTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/2.9 - Grid Settings/GridTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/3.1 - Drag Actions/DiscardAllCardsOperator.cs
./Assets/CardHouse/Tutorial/Lessons/3.5 - Sprite Operators/SpriteOperatorTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/3.5 - Sprite Operators/SpriteVoterTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/4.1 - Transfer Operators/TransferOperatorTutorialUI.cs
./Assets/CardHouse/Tutorial/Lessons/4.2 - Seekers/SeekerTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/4.3 - Event Chains/EventChainsTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/5.1 - Valid Drags/ValidDragTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/5.2 - Valid Drag Gates/MatureCropDragGate.cs
./Assets/CardHouse/Tutorial/Lessons/5.2 - Valid Drag Gates/PhaseLabelUpdater.cs
./Assets/CardHouse/Tutorial/Lessons/5.2 - Valid Drag Gates/Plant.cs
./Assets/CardHouse/Tutorial/Lessons/5.2 - Valid Drag Gates/PlantGrowthScriptable.cs
./Assets/CardHouse/Tutorial/Lessons/5.2 - Valid Drag Gates/WaterPlantAction.cs
./Assets/CardHouse/Tutorial/Lessons/5.2 - Valid Drag Gates/WaterTargetPlantGate.cs
./Assets/CardHouse/Tutorial/Lessons/8.1 - Multiplayer Cameras/PresentationPointTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/8.2 - Multi Board Setup/MultiBoardTutorial.cs
./Assets/CardHouse/Tutorial/Lessons/End/OutLinks.cs
./Assets/CardHouse/Tutorial/Overlay/SandboxManager.cs
./Assets/CardHouse/Tutorial/Overlay/SceneKeeper.cs
./Assets/CardHouse/Tutorial/Overlay/SceneSpawner.cs
./Assets/CardHouse/Tutorial/Overlay/TutorialButton.cs
./Assets/CardHouse/Tutorial/StringListScriptable.cs
./Assets/Plugins/Sirenix/Odin Inspector/Modules/Unity.Mathematics/MathematicsDrawers.cs
./Assets/Plugins/Sirenix/Odin Inspector/Modules/Unity.Mathematics/Sirenix.OdinInspector.Modules.UnityMathematics.asmdef
./Assets/StarShifters/Core/Data/CardDef.cs
./Assets/StarShifters/Core/Data/DeckPreset.cs
./Assets/StarShifters/Core/Data/ThreatDef.cs
./Assets/StarShifters/Core/Data/ThreatPreset.cs
./Assets/StarShifters/Core/Effects/EffectGraph.cs
./Assets/StarShifters/Core/Effects/Nodes/AddEnergyNextTurns.cs
./Assets/StarShifters/Core/Effects/Nodes/AddMovementNode.cs
./Assets/StarShifters/Core/Effects/Nodes/AddShieldNode.cs
./Assets/StarShifters/Core/Effects/Nodes/BuffDefense.cs
./Assets/StarShifters/Core/Effects/Nodes/BuffMovement.cs
./Assets/StarShifters/Core/Effects/Nodes/DelayThreatNode.cs
./Assets/StarShifters/Core/Effects/Nodes/EnergyPenaltyNode.cs
./Assets/StarShifters/Core/Effects/Nodes/KnockBackNode.cs
./Assets/StarShifters/Core/Integration/CardHouse/CardEnergyGate.cs
./Assets/StarShifters/Core/Integration/CardHouse/CardHouseCardLink.cs
./Assets/StarShifters/Core/Integration/CardHouse/CardHouseDeckFactory.cs
./Assets/StarShifters/Core/Integration/CardHouse/CardHouseGameBridge.cs
./Assets/StarShifters/Core/Integration/CardHouse/CardRuntime.cs
./Assets/StarShifters/Core/Integration/CardHouse/PlayerHandController.cs
./Assets/StarShifters/Core/Integration/Threats/ThreatController.cs
./Assets/StarShifters/Core/Integration/Threats/ThreatRuntime.cs
./Assets/StarShifters/Core/Runtime/Debug/EffectGraphDebugRunner.cs
./Assets/StarShifters/Core/Runtime/EffectContext.cs
./Assets/StarShifters/Core/Runtime/GameEnums.cs
./Assets/StarShifters/Core/Runtime/TurnController.cs
./Assets/StarShifters/Editor/CopyGuidUtility.cs
./Assets/Tools/CardOnPlayBinder.cs
./INDEX.md
./Packages/manifest.json
./ProjectSettings/ProjectVersion.txt
```

## Подсказки
- C# скрипты: Assets/StarShifters/**/*.cs
- Пакеты/версия Unity: Packages/manifest.json, ProjectSettings/ProjectVersion.txt
- Документы: Правила/План/Тех стек/Заметки
