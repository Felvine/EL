public  class CharacterProperties {
    private bool invincible = false;
    private CharacterResource health;
    private CharacterResource stamina;
    private CharacterResource mana;

    public CharacterProperties () {
        this.health = new CharacterResource (100, UnityEngine.Color.red);
        this.stamina = new CharacterResource (100, UnityEngine.Color.yellow);
        this.mana = new CharacterResource (100, UnityEngine.Color.blue);
    }
}
