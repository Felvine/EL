class CharacterResource {
    private int value;
    private int maxValue;
    private UnityEngine.Color color;
    private float regenRate;

    public CharacterResource (int maxValueIn, UnityEngine.Color colorIn) {
        this.maxValue = maxValueIn;
        this.value = maxValueIn;
        this.color = colorIn;
        this.regenRate = 0.0f;
    }
}
