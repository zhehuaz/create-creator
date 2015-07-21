using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PsoPlanning.OpPlanning;

///<Author>Zhehua Chang</Author>
///<Date>Jul.12th, 2015</Date>
namespace PsoPlanning.Pso
{
    /// <summary>
    /// The algorithm body of PSO.
    /// </summary>
    /// <typeparam name="PositionEle">Datatype of element in position vector.</typeparam>
    /// <typeparam name="VelocityEle">Datatype of element in velocity vector.</typeparam>
    public class PsoAlgorithm<PositionEle, VelocityEle>
    {
        /// <summary>
        /// Particles in the Pso algorithm.
        /// The amount is set in constructor <seealso cref="PsoAlgorithm(int, int)"/>.
        /// </summary>
        protected BaseParticle<PositionEle, VelocityEle>[] particles;
        /// <summary>
        /// Iterating turns' count.
        /// </summary>
        public int MaxEpochs { get; private set; }

        /// <summary>
        /// The amount of particles.
        /// </summary>
        public int ParticleCount { get; private set; }

        public BaseParticle<PositionEle, VelocityEle> GlobalBestParticle { get; set; }

        /// <summary>
        /// Initialize the particle count and interating count of algorithm.
        /// </summary>
        public PsoAlgorithm(int particleCount = 25, int maxEpochs = 100) {
            this.ParticleCount = particleCount;
            this.MaxEpochs = maxEpochs;

            particles = new BaseParticle<PositionEle, VelocityEle>[ParticleCount];
        }

        /// <summary>
        /// If you want to do something before algorithm run, override this func.
        /// This func is called once before all.
        /// </summary>
        protected virtual void Initialize() { }

        /// <summary>
        /// Entrance of algorithm.
        /// </summary>
        public void run()
        {
            Initialize();
            int curFitness;
            int curGlobalBestFitness = Int32.MinValue;
            for(int i = 0;i < MaxEpochs;i ++)
            {
                for(int j = 0;j < ParticleCount;j ++)
                {
                    curFitness = particles[j].CalFitness();
                    if (curFitness > particles[j].BestFitness)
                    {
                        particles[j].BestFitness = curFitness;

                        if(curFitness > curGlobalBestFitness)
                        {
                            curGlobalBestFitness = curFitness;
                            GlobalBestParticle = particles[j];
                        }
                    } 
                }
                for(int j = 0;j < ParticleCount;j ++)
                {
                    particles[j].UpdateVelocity(GlobalBestParticle);
                    particles[j].AfterVelocityUpdated();
                    particles[j].UpdatePosition();
                    particles[j].AfterPositionUpdated();
                }   
            }
        }
    }
}
