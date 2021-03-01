namespace MakeFs

open Godot
open MakeFs
open MakeFs.Domain.Character

type PlayerPreview3dFs() =
    inherit Spatial()

    let mutable ui: UiManagerFs = null

    // Instanced knight preview scenes
    let mutable strengthKnight: Node = null
    let mutable vitalityKnight: Node = null
    let mutable skillKnight: Node = null

    // The node in which to add the instanced scene
    let mutable container: Node = null

    // The actively shown preview scene
    let mutable preview: Node = null

    let loadPreview (scene: Node): Node =
        if preview <> null then
            container.RemoveChild(preview)

        container.AddChild(scene)
        scene

    [<Export>]
    member val KnightContainerPath: NodePath = null with get, set
    [<Export>]
    member val StrengthKnightScene: PackedScene = null with get, set
    [<Export>]
    member val VitalityKnightScene: PackedScene = null with get, set
    [<Export>]
    member val SkillKnightScene: PackedScene = null with get, set

    override this._Ready() =
        ui <- UiManagerFs.get this
        container <- this.GetNode<Node>(this.KnightContainerPath)
        strengthKnight <- this.StrengthKnightScene.Instance()
        vitalityKnight <- this.VitalityKnightScene.Instance()
        skillKnight <- this.SkillKnightScene.Instance()

        preview <- loadPreview skillKnight

        ui.Connect("build_changed", this, "_OnBuildChanged") |> ignore
    
    member this._OnBuildChanged(build: BuildWrapped) =
        let node =
            match build.Value with
                | Strength -> strengthKnight
                | Vitality -> vitalityKnight
                | Skill -> skillKnight

        preview <- loadPreview node
