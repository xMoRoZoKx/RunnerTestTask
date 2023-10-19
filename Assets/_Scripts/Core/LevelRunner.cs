using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UniTools;
using UnityEngine;

//Enter point for level
public class LevelRunner : ConnectableMonoBehaviour
{
    [SerializeField] private GenerateSettingsConfig generateSettings;
    [SerializeField] private CharacterView characterPrefab;
    [SerializeField] private CinemachineVirtualCamera cmCameraPrefab;
    [SerializeField] private Transform tileRoot, interactableViewRoot;
    private EventStream onUpdate = new EventStream();
    private void Awake()
    {
        var platformGenerator = new PlatformGenerator();

        var character = Instantiate(characterPrefab);
        character.Init(platformGenerator.Direction);

        var camera = Instantiate(cmCameraPrefab);
        camera.Follow = character.transform;
        camera.LookAt = character.transform;

        platformGenerator.Generate(character, generateSettings,
            tile => tile.transform.SetParent(tileRoot),
            interactableView => interactableView.transform.SetParent(interactableViewRoot));

        connections += onUpdate.Subscribe(platformGenerator.UpdateTiles);

        //Show HUD window from prefabs in resources
        WindowManager.Instance.Show<GameHUD>(inst =>
        {
            inst.Show(character);
        });

    }
    private void Update()
    {
        onUpdate.Invoke();
    }
}
