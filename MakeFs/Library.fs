namespace MakeFs

open Godot
open System
open System.Collections.Generic

open MakeFs.Domain

module MakeFs =
    open Character
    
    // A wrapper around Godot.Object. This is necessary because Godot will not
    // marshal any user-defined types that don't inherit Object. Furthermore, Godot
    // will not be able to infer generic types, so in order to use this, you must
    // subclass it as a specialized type
    type GodotWrapped<'t>(value: 't) =
        inherit Godot.Object()
        member this.Value: 't = value

    type BuildWrapped(build: Build) = inherit GodotWrapped<Build>(build)
    type WeaponWrapped(weapon: Weapon) = inherit GodotWrapped<Weapon>(weapon)
    type ModifierWrapped(modifier: Modifier) = inherit GodotWrapped<Modifier>(modifier)
    type CharacterWrapped(char: Character) = inherit GodotWrapped<Character>(char)

    type GameState =
        { PlayerName: string
          PlayerBuild: Build option
          PlayerWeapon: Weapon option
          PlayerModifier: Modifier option
          Enemy: Character }

        static member Init
            (rand: Random)
            (playerName: string)
            (enemyName: string) : GameState =
                
                { PlayerName = playerName
                  PlayerBuild = None
                  PlayerWeapon = None
                  PlayerModifier = None
                  Enemy = Character.Random rand enemyName }

        member this.Player() : Character option =
            match (this.PlayerBuild, this.PlayerWeapon, this.PlayerModifier) with
                | (Some b, Some w, Some m) ->
                    Some
                        { Name = this.PlayerName
                          Build = b
                          Weapon = w
                          Modifier = m }
                | _ -> None

    type GameResult =
        | Won
        | Lost
    
    let gameResult (state: GameState) : GameResult option =
        let enemy = state.Enemy
        match state.Player() with
            | Some player when player > enemy -> Some Won
            | Some player -> Some Lost
            | _ -> None
