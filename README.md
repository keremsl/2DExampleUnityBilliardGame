
# 2D Billiard Game 

This project is case study example for Sisal Company.This example uses GameEvent(Observer) and Singleton pattern mixed software artitechure. GameEvents are supported with ScriptableObjects to minimize dependecies.

## Main Entry Point

OnGameStarted Event calling in GameManager.

```bash
  private void Start () {
        OnGameStarted?.OnEventRaised ();
    }
```

  
## Gameplay

User should press and hold the white ball then white ball moves toward random direction with `AddForce()` function.

  
## Game Events

Game Events are triggered when needed with OnEventRaised()

`OnBallCollide`

`OnDataReady`

`OnGameStarted`

`OnPlayerNameSetted`

  
## Singletons

`GameManager`

`SoundManager`
## Lifecycle

OnGameStarted Event Raised -> Loading Screen Show -> After Data Ready , OnDataReady Event Raised ->  
PlayerNameRequest Window Show -> After Name Ready -> Main Game Loop Start & Greeting Text Appeared
## External Packages

Dotween : To make animations and make better game feeling

Newtonsoft.Json : To deserialize and assign data
## Resources

Table and ball graphics : freepik.com

Sounds : https://www.youtube.com/watch?v=Jw2bL_YDRyY