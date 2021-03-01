namespace MakeFs.Domain

open System
open System.Collections.Generic

module Character =
    [<CustomComparison; StructuralEquality>]
    type Build  =
        | Strength
        | Vitality
        | Skill

        interface IComparable<Build> with
            member this.CompareTo other =
                match (this, other) with
                    | (Strength, Strength) -> 0
                    | (Strength, Vitality) -> -1
                    | (Strength, Skill) -> 1
                    | (Vitality, Strength) -> 1
                    | (Vitality, Vitality) -> 0
                    | (Vitality, Skill) -> -1
                    | (Skill, Strength) -> -1
                    | (Skill, Vitality) -> 1
                    | (Skill, Skill) -> 0

        member this.HelpText(): string =
            match this with
                | Strength ->
                    "Strength Build: Primarily uses strong physical force. Has a \
                     powerful attack, but is vulnerable to tougher opponents."
                | Vitality ->
                    "Vitality Build: Primarily uses defensive tactics. Can absorb \
                     powerful attacks, but is vulnerable to quicker apponents."
                | Skill ->
                    "Skill Build: Primarily uses quickness and counter-attacks. Can \
                    easily out-manuever defensive tactics, but is vulnerable to \
                    strong opponents."

    [<CustomComparison; StructuralEquality>]
    type Weapon =
        | Sword
        | Axe
        | Warhammer

        interface IComparable<Weapon> with
            member this.CompareTo other =
                match (this, other) with
                    | (Sword, Sword) -> 0
                    | (Sword, Axe) -> -1
                    | (Sword, Warhammer) -> 1
                    | (Axe, Sword) -> -1
                    | (Axe, Axe) -> 0
                    | (Axe, Warhammer) -> 1
                    | (Warhammer, Sword) -> -1
                    | (Warhammer, Axe) -> 1
                    | (Warhammer, Warhammer) -> 0

        member this.HelpText(): string =
            match this with
                | Sword ->
                    "Sword: A weapon for knights. Can easily cut through blunt weapons \
                     but is vulnerable to heavy sharp weapons."
                | Axe ->
                    "Axe: A heavy blade. Is effective against other sharp weapons but \
                     is vulnearable to heapy blunt weapons."
                | Warhammer ->
                    "Warhammer: A very heavy weapon used for pure brute force. Can \
                    easily destroy other heavy weapons, but is no match for quick finesse \
                    weapons."

    [<CustomComparison; StructuralEquality>]
    type Modifier =
        | Fire
        | Ice
        | Lightning

        interface IComparable<Modifier> with
            member this.CompareTo other =
                match (this, other) with
                    | (Fire, Fire) -> 0
                    | (Fire, Ice) -> -1
                    | (Fire, Lightning) -> 1
                    | (Ice, Fire) -> 1
                    | (Ice, Ice) -> 0
                    | (Ice, Lightning) -> -1
                    | (Lightning, Fire) -> -1
                    | (Lightning, Ice) -> 1
                    | (Lightning, Lightning) -> 0

        member this.HelpText(): string =
            match this with
                | Fire ->
                    "Fire Damage: Creates devastating destruction beyond any other \
                     force, but can be cancelled by very cold objects."
                | Ice ->
                    "Ice Damage: Particularly effective in countering heat damage \
                     but is no match for other destructive forces."
                | Lightning ->
                    "Lightning: Creates isolated destruction close to other damaging forces \
                     and can easily destroy any solid object."

    let build (v: string) : Build =
        match v with
            | "Strength" -> Strength
            | "Vitality" -> Vitality
            | "Skill" -> Skill
            | _ -> raise (System.ArgumentException())

    let weapon (v: string) : Weapon =
        match v with
            | "Sword" -> Sword
            | "Axe" -> Axe
            | "Warhammer" -> Warhammer
            | _ -> raise (System.ArgumentException())

    let modifier (v: string) : Modifier =
        match v with
            | "Fire" -> Fire
            | "Ice" -> Ice
            | "Lightning" -> Lightning
            | _ -> raise (System.ArgumentException())

open Character

[<CustomComparison; StructuralEquality>]
type Character =
    { Name: string
      Build : Build
      Weapon : Weapon
      Modifier : Modifier }

    static member Random (rand: Random) (name: string) : Character =
        let builds = [|Strength; Vitality; Skill|]
        let weapons = [|Sword; Axe; Warhammer|]
        let modifiers = [|Fire; Ice; Lightning|]
 
        let build = builds.[rand.Next(builds.Length)]
        let weapon = weapons.[rand.Next(weapons.Length)]
        let modifier = modifiers.[rand.Next(weapons.Length)]
        
        { Name = name
          Build = build
          Weapon = weapon
          Modifier = modifier }

    interface IComparable<Character> with
        member this.CompareTo other =
            let cmp getter =
                (getter this :> IComparable<_>).CompareTo (getter other)

            cmp (fun c -> c.Build) +
            cmp (fun c -> c.Weapon) +
            cmp (fun c -> c.Modifier)

    interface IComparable with
        member this.CompareTo other =
            match other with
                | null -> 1
                | :? Character as other ->
                    (this :> IComparable<_>).CompareTo other
                | _ -> invalidArg "obj" "Not a Character"
