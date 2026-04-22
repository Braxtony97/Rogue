# Rogue

**Тестовый проект**


<img width="1606" height="906" alt="playmode" src="https://github.com/user-attachments/assets/e8c157e0-e1e9-4a8e-a3dc-6017cc33f046" />

**Архитектурные решения и паттерны:**  
- **EntryPoint** - единая точка входа в проект
- **EventAggregator** - система событий для взаимодействия между компонентами
- **MVVM** - (model - CharacterModel, AbilityModel, ModifierModel - хранят данные и бизнес-логику, 
          View - CharacterPanelView, CharacterView, AbilityView, ModifierView - отвечают за отрисовку UI, 
          ViewModel - CharacterViewModel, AbilityViewModel, ModifierViewModel - связывают Model и View)
- **Object Pooling** - (UI элементы) - префабы экранов и панелей создаются один раз и переиспользуются через Dictionary<Enum, UIElement>
- **ScriptableObject как DataProvider** - хранение статических данных
