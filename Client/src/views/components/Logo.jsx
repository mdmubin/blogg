/* eslint-disable object-curly-newline */

import React from 'react';
import Types from 'prop-types';

function Logo({ w, h, style, className }) {
  return (
    <img
      src="/logo.svg"
      alt="logo"
      className={className}
      width={w}
      height={h}
      style={style}
    />
  );
}

Logo.propTypes = {
  w: Types.number,
  h: Types.number,
  // eslint-disable-next-line react/forbid-prop-types
  style: Types.any,
  className: Types.string,
};

Logo.defaultProps = {
  w: 100,
  h: 100,
  style: {},
  className: '',
};

export default Logo;
