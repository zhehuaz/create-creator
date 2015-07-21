using System;

///<Author>Zhehua Chang</Author>
///<Date>Jul.12th, 2015</Date>
namespace PsoPlanning.Pso
{
    /// <summary>
    /// In PSO algorithm, a particle is a bird searching food.
    /// We use position, velocity and the fitness of the position at certain time 
    /// to describe a bird, or to say, a particle.
    /// 
    /// Classes inheriting this class are supposed to clarify the code form of 
    /// position and velocity, like how many dimens, or what is the element of
    /// position and velocity vector, whose datatype is Ele.
    /// </summary>
    /// <typeparam name="PositionEle">Datetype of the element in position vector.</typeparam>
    /// <typeparam name="VelocityEle">Datatype of the element in velocity vector.</typeparam>
    public abstract class BaseParticle<PositionEle, VelocityEle> : IComparable
    {
        /// <summary>
        /// The position where the particle exists.
        /// </summary>
        public PositionEle[] CurPosition { get; protected set;}

        /// <summary>
        /// The velocity stands for where to go at next step.
        /// </summary>
        public VelocityEle[] CurVelocity{ get; protected set;}

        /// <summary>
        /// The position in history where the particle get the best fitness.
        /// </summary>
        public PositionEle[] BestPosition { get; protected set; }

        /// <summary>
        /// The best fitness in history of this particle.
        /// </summary>
        public int BestFitness { get; set; }

        /// <summary>
        /// Constructor.
        /// Allocate Memory for position and velocity vector for future use.
        /// </summary>
        /// <param name="positionDimen"> Position dimension defines the room of position vector to allocate.</param>
        /// <param name="velocityDimen"> Velocity dimension defines the room of velocity vector to allocate.</param>
        public BaseParticle(int positionDimen, int velocityDimen)
        {
            CurPosition = new PositionEle[positionDimen];
            CurVelocity = new VelocityEle[velocityDimen];
            BestPosition = new PositionEle[positionDimen];
            BestFitness = Int32.MinValue;
        }

        /// <summary>
        /// Defines how to update velocity of this particle in the current situation.
        /// </summary>
        public abstract void UpdateVelocity(BaseParticle<PositionEle, VelocityEle> bestParticle);
        /// <summary>
        /// Invoked after UpdateVelocity() is called, usaully for check bound of 
        /// velocity.
        /// </summary>
        public abstract void AfterVelocityUpdated();
        /// <summary>
        /// Update position with velocity.
        /// </summary>
        public abstract void UpdatePosition();

        /// <summary>
        /// After position update
        /// </summary>
        public abstract void AfterPositionUpdated();
        /// <summary>
        /// Calculate fitness with the position.
        /// </summary>
        /// <returns> Returns fitness as int.Datetype of return value except int should be cast to int uniformly
        /// so that paricle is comparable.</returns>
        public abstract int CalFitness();

        /// <summary>
        /// Impelement from IComparable, used when sorted by BestFiness in PSO.
        /// </summary>
        /// <param name="obj">This object is suppose to be a BaseParticle.
        /// If not, ArgumentException will be thrown.</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            BaseParticle<PositionEle, VelocityEle> particle = obj as BaseParticle<PositionEle, VelocityEle>;
            if (particle != null)
            {
                if (particle.BestFitness > this.BestFitness)
                    return 1;
                else if (particle.BestFitness < this.BestFitness)
                    return -1;
                else
                    return 0;
            }
            else
                throw new ArgumentException("Object is not a Particle");
        }
    }
}
