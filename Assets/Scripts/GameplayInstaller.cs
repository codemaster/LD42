using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller<GameplayInstaller>
{
	[SerializeField]
	private Transform _sushiContainer;

	[SerializeField]
	private RectTransform _uiArea;
	
    public override void InstallBindings()
    {
		Container.BindFactory<SushiTreadmill, Sushi, Sushi.Factory>()
			.FromFactory<SushiFactory>()
			.NonLazy();

		Container.BindMemoryPool<Sushi, SushiPool>()
			.WithInitialSize(5)
			.WithFactoryArguments(_sushiContainer, _uiArea)
			.FromNewComponentOnNewGameObject();

		Container.Bind<Timer>().FromComponentInHierarchy().AsSingle();
		Container.Bind<SushiSpawner>().FromComponentInHierarchy().AsSingle();
		Container.Bind<SushiTreadmill>().FromComponentInHierarchy().AsSingle();

		Container.BindInterfacesAndSelfTo<InputHandler>().FromNewComponentOnNewGameObject().AsSingle();
    }
}