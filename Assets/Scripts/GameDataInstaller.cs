using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameDataInstaller", menuName = "Installers/GameDataInstaller")]
public class GameDataInstaller : ScriptableObjectInstaller<GameDataInstaller>
{
	public SushiInfo[] SushiInfos;

    public override void InstallBindings()
    {
		Container.BindInstances(SushiInfos);
    }
}