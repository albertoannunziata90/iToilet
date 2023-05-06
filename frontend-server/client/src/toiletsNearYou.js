import React from "react";
import DefaultMap from "./Map";

const ToiletsNearYou = () => {
  return (
    <section>
      <div>
        <section class="hero is-link">
          <div class="hero-body">
            <h2 class="title is-2">IToilet! Need to pee?</h2>
          </div>
        </section>
        <div class="container">
          <DefaultMap></DefaultMap>
        </div>
      </div>
      <div class="container">
        <div>
          <h3 class="title is-4">Bagni da non perdere</h3>
        </div>
        <div class="columns">
          <div class="column">
            <div class="card">
              <div class="card-image">
                <figure class="image is-4by3">
                  <img src="/bagnoSulMare.jpg" alt="Bagno sul Mare" />
                </figure>
              </div>
              <div class="card-content">
                <div class="content">Vista mare</div>
              </div>
            </div>
          </div>
          <div class="column">
            <div class="card">
              <div class="card-image">
                <figure class="image is-4by3">
                  <img src="/wc-a-bocca.jpg" alt="Rolling Stones" />
                </figure>
              </div>
              <div class="card-content">
                <div class="content">Sei proprio rock!</div>
              </div>
            </div>
          </div>
          <div class="column">
            <div class="card">
              <div class="card-image">
                <figure class="image is-4by3">
                  <img src="/pacman.jpg" alt="Game" />
                </figure>
              </div>
              <div class="card-content">
                <div class="content">
                  Riesci a farla prima che i mostri ti acchiappino?
                </div>
              </div>
            </div>
          </div>
          <div class="column">
            <div class="card">
              <div class="card-image">
                <figure class="image is-4by3">
                  <img src="/king.jpeg" alt="Re" />
                </figure>
              </div>
              <div class="card-content">
                <div class="content">Sentirsi un re</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
  );
};

export default ToiletsNearYou;
