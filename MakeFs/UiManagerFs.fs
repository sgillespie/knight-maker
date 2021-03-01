namespace MakeFs

open System

open Godot
open MakeFs
open MakeFs.Domain

[<AllowNullLiteral>]
type UiManagerFs() =
    inherit Node()

    member val Rand: Random = Random() with get

    member val GameState: GameState option = None with get, set

    override this._Ready() =
        this.Connect("build_changed", this, "_OnBuildChanged") |> ignore
        this.Connect("weapon_changed", this, "_OnWeaponChanged") |> ignore
        this.Connect("modifier_changed", this, "_OnModifierChanged") |> ignore

    member this.Quit() =
        this.GetTree().Quit()

    member this._OnBuildChanged(build: BuildWrapped) =
        this.GameState <-
            this.GameState
            |> Option.map (fun state -> { state with PlayerBuild = Some build.Value })

    member this._OnWeaponChanged(weapon: WeaponWrapped) =
        this.GameState <-
            this.GameState
            |> Option.map (fun st -> { st with PlayerWeapon = Some weapon.Value })

    member this._OnModifierChanged(modifier: ModifierWrapped) =
        this.GameState <-
            this.GameState
            |> Option.map (fun st -> { st with PlayerModifier = Some modifier.Value })

module UiManagerFs =
    let get (node: Node) = node.GetNode<UiManagerFs>(new NodePath("/root/UIManager"))
          
