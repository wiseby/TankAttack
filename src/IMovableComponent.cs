namespace TankAttack
{
    public interface IMovableComponent
    {
        public bool CanAccelerate { get; set; }
        public bool CanReverse { get; set; }
        public bool CanRotate { get; set; }
        public bool IsAccelerating { get; set; }
        public bool IsReversing { get; set; }

        public void Accelerate();
        public void Reverse();
        public void HullRotate(bool clockWise);
        public void TurretRotate(bool clockWise);
    }
}