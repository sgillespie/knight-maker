using Godot;
using System;

using MakeFs;

using static MakeFs.MakeFs;

public class UiManager : UiManagerFs {
    [Signal]
    public delegate void battle_pressed();

    [Signal]
    public delegate void build_changed(BuildWrapped build);

    [Signal]
    public delegate void weapon_changed(WeaponWrapped weapon);

    [Signal]
    public delegate void modifier_changed(ModifierWrapped modifier);

    [Signal]
    public delegate void build_mouse_enter(BuildWrapped build);

    [Signal]
    public delegate void weapon_mouse_enter(WeaponWrapped weapon);

    [Signal]
    public delegate void modifier_mouse_enter(ModifierWrapped modifier);
    
    [Signal]
    public delegate void quit();

    [Signal]
    public delegate void close_modal();
}
